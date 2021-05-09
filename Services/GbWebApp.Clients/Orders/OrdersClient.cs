using System.Net.Http;
using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using GbWebApp.Clients.Base;
using System.Threading.Tasks;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _logger;

        public OrdersClient(IConfiguration cfg, ILogger<OrdersClient> logger) : base(cfg, WebApiRoutes.OrdersAPI) => _logger = logger;

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{userName}");

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<OrderDTO> CreateOrder(string userName, OrderModel orderModel)
        {
            _logger.LogInformation("Creating new order...");
            using (_logger.BeginScope("*** CREATING ORDER SCOPE ***"))
            {
                var result = (await PostAsync($"{Address}/{userName}", orderModel)).Content.ReadAsAsync<OrderDTO>();
                _logger.LogInformation($"...completed successfully! id={result.Result.Id}");
                return await result;
            }
        }
    }
}
