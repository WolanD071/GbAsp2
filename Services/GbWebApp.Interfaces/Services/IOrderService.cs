using System.Collections.Generic;
using System.Threading.Tasks;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
