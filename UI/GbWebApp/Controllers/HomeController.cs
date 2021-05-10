using Microsoft.AspNetCore.Mvc;
using System;

namespace GbWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult _404() => View("404");
        public ActionResult Throw() => throw new ApplicationException("Test error!");
    }
}
