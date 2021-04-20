using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GbWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult _404() => View("404");
    }
}
