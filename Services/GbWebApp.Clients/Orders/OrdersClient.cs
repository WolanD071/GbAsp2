using System.Net.Http;
using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using GbWebApp.Clients.Base;
using System.Threading.Tasks;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace GbWebApp.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebApiRoutes.OrdersAPI) { }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}");

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<OrderDTO> CreateOrder(string UserName, OrderModel orderModel)
        {
            var response = await PostAsync($"{Address}/{UserName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }
    }
}
