using Microsoft.AspNetCore.Mvc;
using GbWebApp.Interfaces.TestAPI;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GbWebApp.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _valuesService;

        public WebAPIController(IValuesService valuesService) => _valuesService = valuesService;

        public IActionResult Index() => View(_valuesService.Get());

        public IActionResult GetById(int id)
        {
            ViewBag.Id = id;
            return View(model: _valuesService.Get(id));
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IActionResult NewVal() => View();

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult NewVal([FromForm] string value)
        {
            _valuesService.Create(value);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DelById(int id)
        {
            ViewBag.Id = id;
            return View(model: _valuesService.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DeleteValue(int id)
        {
            if (_valuesService.Get(id) == string.Empty) return NotFound();
            _valuesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
