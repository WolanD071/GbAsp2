using System;
using System.Linq;
using GbWebApp.Domain.DTO;
using GbWebApp.DAL.Context;
using System.Threading.Tasks;
using GbWebApp.Domain.Entities;
using GbWebApp.Services.Mappers;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Domain.Entities.Identity;

namespace GbWebApp.Services.Services.InDB
{
    public class InDbOrderData : IOrderService
    {
        private readonly GbWebAppDB _db;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public InDbOrderData(GbWebAppDB db, UserManager<User> userManager, ILogger<InDbOrderData> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           //.ThenInclude(p => p.Product)
           .Where(order => order.User.UserName == userName)
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

            _logger.LogInformation($"Creating new order for user '{user.UserName}'...");
            using (_logger.BeginScope("*** CREATING ORDER SCOPE ***"))
            {
                await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

                var order = new Order { Name = orderModel.Order.Name, Address = orderModel.Order.Address, Phone = orderModel.Order.Phone, User = user };
                foreach (var item in orderModel.Items)
                {
                    var product = await _db.Products.FindAsync(item.Id);
                    if (product is null) continue;
                    var orderItem = new OrderItem { Order = order, Price = product.Price, Quantity = item.Quantity, Product = product };
                    order.Items.Add(orderItem);
                }

                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("...completed successfully!");
                return order.ToDTO();
            }
        }
    }
}
