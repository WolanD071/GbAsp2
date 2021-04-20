using System;
using System.Linq;
using GbWebApp.ViewModels;
using GbWebApp.DAL.Context;
using System.Threading.Tasks;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Infrastructure.Interfaces;

namespace GbWebApp.Infrastructure.Services.InDB
{
    public class InDbOrdertData : IOrderService
    {
        private readonly GbWebAppDB _db;
        private readonly UserManager<User> _userManager;

        public InDbOrdertData(GbWebAppDB db, UserManager<User> UserManager)
        {
            _db = db;
            _userManager = UserManager;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync();

        public async Task<Order> GetOrderById(int id) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id);


        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Error! User '{UserName}' not found in DB");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            { Name = OrderModel.Name, Address = OrderModel.Address, Phone = OrderModel.Phone, User = user };

            var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            var cart_products = await _db.Products
               .Where(p => product_ids.Contains(p.Id))
               .ToArrayAsync();

            var c_items = Cart.Items.Join(cart_products, cart_item => cart_item.Product.Id, product => product.Id, (cart_item, product)
                => new OrderItem { Order = order, Product = product, Price = product.Price, Quantity = cart_item.Quantity }).ToArray();

            foreach (var item in c_items)
                order.Items.Add(item);

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
    }
}
