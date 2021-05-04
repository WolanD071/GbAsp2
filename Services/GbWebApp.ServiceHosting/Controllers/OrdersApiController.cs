using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    [Route(WebApiRoutes.OrdersAPI)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => await _orderService.GetUserOrders(UserName);

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _orderService.GetOrderById(id);

        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] OrderModel orderModel) =>
            await _orderService.CreateOrder(UserName, orderModel);
    }
}
