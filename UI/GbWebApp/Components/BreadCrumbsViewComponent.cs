using System;
using GbWebApp.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Services.Mappers;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BreadCrumbsViewComponent(IProductService productService) => _productService = productService;

        public async Task<IViewComponentResult> InvokeAsync(string displayActionName = "")
        {
            var model = new BreadCrumbsViewModel
            {
                Controller = ViewContext.RouteData.Values["controller"]?.ToString(),
                ActionName = ViewContext.RouteData.Values["action"]?.ToString(),
                ActionDisp = displayActionName == "" || displayActionName is null
                    ? ViewContext.RouteData.Values["action"]?.ToString() : displayActionName
            };

            if (model.Controller == "Shop")
            {
                if (int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out var pId))
                {
                    if (pId <= 0)
                        throw new IndexOutOfRangeException();
                    model.Product = _productService.GetProductById(pId).FromDTO();
                    if (model.Product.BrandId != null)
                        model.Brand = _productService.GetBrandById((int)model.Product.BrandId).FromDTO();
                    model.Section = _productService.GetSectionById(model.Product.SectionId).FromDTO();
                }
                else
                {
                    if (int.TryParse(Request.Query["BrandId"].ToString(), out var bId))
                        model.Brand = _productService.GetBrandById(bId).FromDTO();
                    if (int.TryParse(Request.Query["SectionId"].ToString(), out var sId))
                        model.Section = _productService.GetSectionById(sId).FromDTO();
                }
                if (model.Section?.ParentId != null)
                    model.Section.Parent = _productService.GetSectionById((int)model.Section.ParentId).FromDTO();
            }

            return View(model);
        }
    }
}
