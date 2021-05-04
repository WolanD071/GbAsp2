using GbWebApp.Domain.DTO;
using System.Threading.Tasks;
//using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string UserName, OrderModel orderModel);
    }
}
