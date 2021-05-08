using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    /// <summary> orders management </summary>
    [Route(WebApiRoutes.OrdersAPI)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        /// <summary> getting all the orders of specified user </summary>
        /// <param name="UserName"> user's name </param>
        /// <returns> list of orders </returns>
        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => await _orderService.GetUserOrders(UserName);

        /// <summary> getting specified order </summary>
        /// <param name="id"> id of the order </param>
        /// <returns> order found </returns>
        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _orderService.GetOrderById(id);

        /// <summary> creation of the new order </summary>
        /// <param name="UserName"> user - order's owner </param>
        /// <param name="orderModel"> model of the order </param>
        /// <returns> order created </returns>
        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] OrderModel orderModel) =>
            await _orderService.CreateOrder(UserName, orderModel);
    }
}
