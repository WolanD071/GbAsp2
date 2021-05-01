using Microsoft.AspNetCore.Authorization;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Mappers;

namespace GbWebApp.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductData __productData;

        public ProductsController(IProductData ProductData)
            => __productData = ProductData;

        public IActionResult Edit(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id == 0)
                return View(new ProductViewModel { Id = 0 });
            return __productData.GetProductById(id) is { } product ? View(product.ToView()) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return View(model);
            Product prod = model.FromView();
            if (model.Id == 0)
                __productData.Add(prod);
            else
                __productData.Update(prod);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
            => View("Edit", new ProductViewModel { Id = 0 });

        public IActionResult Index()
            => View(__productData.GetProducts().Include(p => p.Section).Include(p => p.Brand));

        [HttpPost]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (__productData.GetProductById(id) is null)
                return NotFound();
            __productData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
