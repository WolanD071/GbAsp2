using GbWebApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GbWebApp.ViewModels;
using System.Linq;
using GbWebApp.DAL.Context;

namespace GbWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly GbWebAppDB __db;
        public UsersController(GbWebAppDB db) { __db = db; }
        public IActionResult Index() => View(__db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name));
        public async Task<IActionResult> Orders([FromServices] IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrders(User.Identity!.Name);
            return View(orders.Select(o => new UserOrderViewModel
            { Id = o.Id, Name = o.Name, Phone = o.Phone, Address = o.Address, TotalPrice = o.Items.Sum(item => item.Price * item.Quantity) }));
        }
    }
}