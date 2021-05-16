using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductService _productData;

        public SectionsViewComponent(IProductService productData) => _productData = productData;

        public IViewComponentResult Invoke(string sectionId, bool combo = false, int id = 0)  // params are for admin dashboard
        {
            var secId = int.TryParse(sectionId, out var sid) ? sid : (int?)null;
            var sections = GetAllSections(secId, out var parSecId);

            ViewData["ParentSectionId"] = parSecId;
            ViewBag.SectionId = secId; // the same

            if (combo)
                if (id != 0)
                    return View("ComboId", (id, sections/*GetChildSections()*/));
                else
                    return View("ComboNew", sections/*GetChildSections()*/);
            return View(sections);
        }

        private IEnumerable<SectionViewModel> GetAllSections(int? sectionId, out int? parentSectionId)
        {
            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);

            parentSectionId = null;

            var sections = _productData.GetSections();
            var parentSections = sections.Where(s => s.ParentId == null);
            var parentSectionsViews = parentSections.Select(s => new SectionViewModel
                { Id = s.Id, Name = s.Name, Order = s.Order, ProductCount = s.ProductCnt, /*ProductCount = s.Products.Count()*/ }).ToList();

            foreach (var parentSection in parentSectionsViews)
            {
                var childs = sections.Where(s => s.ParentId == parentSection.Id);
                foreach (var childSection in childs)
                {
                    if (childSection.Id == sectionId) parentSectionId = childSection.ParentId;
                    parentSection.ChildSections.Add(new SectionViewModel
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        Parent = parentSection,
                        ProductCount = childSection.ProductCnt, /*ProductCount = child_section.Products.Count(),*/
                    });
                }
                parentSection.ChildSections.Sort(OrderSortMethod);
            }
            parentSectionsViews.Sort(OrderSortMethod);

            return parentSectionsViews;
        }

        private IEnumerable<SectionViewModel> GetChildSections()
        {
            int IdSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Id, b.Id);

            var sections = _productData.GetSections();
            var childSections = sections.Where(s => s.ParentId != null);
            var childSectionsViews = childSections.Select(s => new SectionViewModel
            { Id = s.Id, Name = s.Name, Order = s.Order, ProductCount = s.ProductCnt, /*ProductCount = s.Products.Count()*/ }).ToList();
            childSectionsViews.Sort(IdSortMethod);

            return childSectionsViews;
        }
    }
}
