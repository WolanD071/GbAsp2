using System.Collections.Generic;
using GbWebApp.Domain.Entities;
using System.Threading.Tasks;
using GbWebApp.ViewModels;

namespace GbWebApp.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
