using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities.Identity;

namespace GbWebApp.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
