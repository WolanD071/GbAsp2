using GbWebApp.Infrastructure.Interfaces;
using GbWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbWebApp.Components
{
    //[ViewComponent(Name = "somename")] // in case when class isn't inherited from ViewComponent
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(bool combo = false, int id = 0)
        {
            if (combo)
                if (id != 0)
                    return View("ComboId", (id, GetBrands()));
                else
                    return View("ComboNew", GetBrands());
            return View(GetBrands());
        }

        IEnumerable<BrandsViewModel> GetBrands() =>
            _ProductData.GetBrands()
               .OrderBy(brand => brand.Order)
               .Select(brand => new BrandsViewModel
               {
                   Id = brand.Id,
                   Name = brand.Name,
                   Count = brand.Products.Count(),
               });
    }
}
