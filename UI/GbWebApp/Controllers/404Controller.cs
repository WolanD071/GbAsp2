﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbWebApp.Controllers
{
    public class _404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
