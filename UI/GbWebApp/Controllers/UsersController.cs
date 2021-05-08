using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace GbWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //readonly GbWebAppDB __db;
        //public UsersController(GbWebAppDB db) { __db = db; }
        //public IActionResult Index() => View(/*__db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)*/);
        public async Task<IActionResult> Index([FromServices] /*IUsersService*/ IUserStore<User> user)
        {
            User u = await user.FindByNameAsync(User.Identity!.Name, CancellationToken.None);
            return View(u);
        }

        public async Task<IActionResult> Orders([FromServices] IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrders(User.Identity!.Name);
            return View(orders.Select(o => new UserOrderViewModel
            { Id = o.Id, Name = o.Name, Phone = o.Phone, Address = o.Address, TotalPrice = o.Items.Sum(item => item./*FromDTO().*/Price * item.Quantity) }));
        }
    }
}
