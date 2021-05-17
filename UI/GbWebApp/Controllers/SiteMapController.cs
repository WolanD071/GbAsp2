using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbWebApp.Interfaces.Services;
using SimpleMvcSitemap;

namespace GbWebApp.Controllers
{
    public class SiteMapController : ControllerBase
    {
        public IActionResult Index([FromServices] IProductService productData)
        {
            var nodes = new List<SitemapNode>
            {
                new (Url.Action("Index", "Home")),
                new (Url.Action("_404", "Home")),
                new (Url.Action("Throw", "Home")),
                new (Url.Action("Index", "_404")),
                new (Url.Action("Index", "Blog")),
                new (Url.Action("BlogSingle", "Blog")),
                new (Url.Action("Index", "Contacts")),
                new (Url.Action("Index", "Shop")),
                new (Url.Action("ShopDetails", "Shop")),
                new (Url.Action("ShopCheckOut", "Shop")),
                new (Url.Action("ShopCart", "Shop")),
                new (Url.Action("ShopLogin", "Shop")),
                new (Url.Action("Index", "WebAPI")),
                new (Url.Action("GetById", "WebAPI")),
                new (Url.Action("Index", "SiteMap"))
            };

            // generate nodes via LINQ
            nodes.AddRange(productData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Shop", new { SectionId = s.Id }))));
            nodes.AddRange(productData.GetBrands().Select(b => new SitemapNode(Url.Action("Index", "Shop", new { BrandId = b.Id }))));

            // or such way - through loop
            foreach (var product in productData.GetProducts())
                nodes.Add(new SitemapNode(Url.Action("Index", "Shop", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
