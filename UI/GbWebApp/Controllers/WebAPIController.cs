using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Interfaces.TestAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GbWebApp.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService __valuesService;

        public WebAPIController(IValuesService valuesService) => __valuesService = valuesService;

        public IActionResult Index() => View(__valuesService.Get());

        public IActionResult GetById(int id)
        {
            ViewBag.Id = id;
            return View(model: __valuesService.Get(id));
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IActionResult NewVal() => View();

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult NewVal([FromForm] string value)
        {
            __valuesService.Create(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DelById(int id)
        {
            ViewBag.Id = id;
            return View(model: __valuesService.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DeleteValue(int id)
        {
            if (__valuesService.Get(id) == string.Empty)
                return NotFound();
            __valuesService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
