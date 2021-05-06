using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbWebApp.DAL.Context;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GbWebApp.Services.Services.InDB
{
    public class InDbOrderData : IOrderService
    {
        private readonly GbWebAppDB _db;
        private readonly UserManager<User> _userManager;

        public InDbOrderData(GbWebAppDB db, UserManager<User> UserManager)
        {
            _db = db;
            _userManager = UserManager;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           //.ThenInclude(p => p.Product)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync()).Select(o => o.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           //.ThenInclude(p => p.Product)
           .FirstOrDefaultAsync(order => order.Id == id)).ToDTO();


        public async Task<OrderDTO> CreateOrder(string userName, OrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) throw new InvalidOperationException($"Error! User '{userName}' not found in DB");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            { Name = orderModel.Order.Name, Address = orderModel.Order.Address, Phone = orderModel.Order.Phone, User = user };

            foreach (var item in orderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null) continue;

                var order_item = new OrderItem { Order = order, Price = product.Price, Quantity = item.Quantity, Product = product/*, ProdId = product.Id*/ };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDTO();
        }
    }
}
