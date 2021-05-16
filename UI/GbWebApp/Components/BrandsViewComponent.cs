using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using GbWebApp.ViewModels;

namespace GbWebApp.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productData;

        public BrandsViewComponent(IProductService productData) => _productData = productData;

        public IViewComponentResult Invoke(string brandId, bool combo = false, int id = 0)  // params are for admin dashboard
        {
            if (combo)
                if (id != 0)
                    return View("ComboId", (id, GetBrands()));
                else
                    return View("ComboNew", GetBrands());
            return View(new BrandsViewModelId { Brands = GetBrands(), BrandId = int.TryParse(brandId, out var bid) ? bid : (int?)null });
        }

        IEnumerable<BrandsViewModel> GetBrands() =>
            _productData.GetBrands()
               .OrderBy(brand => brand.Order)
               .Select(brand => new BrandsViewModel
               {
                   Id = brand.Id,
                   Name = brand.Name,
                   Count = brand.ProductCnt,//Count = brand.Products.Count(),
               });
    }
}
