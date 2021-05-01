using GbWebApp.Interfaces.TestAPI;
using Microsoft.AspNetCore.Mvc;

namespace GbWebApp.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService __valuesService;

        public WebAPIController(IValuesService valuesService) => __valuesService = valuesService;

        public IActionResult Index() => View(__valuesService.Get());
    }
}
