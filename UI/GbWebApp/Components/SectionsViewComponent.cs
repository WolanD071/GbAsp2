using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.Components
{
    //[ViewComponent(Name = "somename")] // in case when class isn't inherited from ViewComponent
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductService _ProductData;

        public SectionsViewComponent(IProductService ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(bool combo = false, int id = 0)
        {
            if (combo)
                if (id != 0)
                    return View("ComboId", (id, GetAllSections()/*GetChildSections()*/));
                else
                    return View("ComboNew", GetAllSections()/*GetChildSections()*/);
            return View(GetAllSections());
        }

        IEnumerable<SectionViewModel> GetAllSections()
        {
            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);

            var sections = _ProductData.GetSections();
            var parent_sections = sections.Where(s => s.ParentId == null);
            var parent_sections_views = parent_sections.Select(s => new SectionViewModel
                { Id = s.Id, Name = s.Name, Order = s.Order, ProductCount = s.ProductCnt, /*ProductCount = s.Products.Count()*/ }).ToList();
            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in childs)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section,
                        ProductCount = child_section.ProductCnt, /*ProductCount = child_section.Products.Count(),*/
                    });
                parent_section.ChildSections.Sort(OrderSortMethod);
            }
            parent_sections_views.Sort(OrderSortMethod);
            return parent_sections_views;
        }

        IEnumerable<SectionViewModel> GetChildSections()
        {
            int IdSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Id, b.Id);

            var sections = _ProductData.GetSections();
            var child_sections = sections.Where(s => s.ParentId != null);
            var child_sections_views = child_sections.Select(s => new SectionViewModel
               { Id = s.Id, Name = s.Name, Order = s.Order, ProductCount = s.ProductCnt, /*ProductCount = s.Products.Count()*/ }).ToList();
            child_sections_views.Sort(IdSortMethod);
            return child_sections_views;
        }
    }
}
