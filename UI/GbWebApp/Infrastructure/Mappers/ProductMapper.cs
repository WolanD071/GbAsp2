using GbWebApp.Domain.Entities;
using System.Linq;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null ? null : new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            BrandId = product.BrandId ?? 0,
            Brand = product.Brand?.Name,
            SectionId = product.SectionId,
            Section = product.Section?.Name,
        };

        public static IQueryable<ProductViewModel> ToView(this IQueryable<Product> products)
            //=> (IQueryable<ProductViewModel>)products.Select(ToView); // invalid cast exception at runtime
            => products.Select(ToView).AsQueryable<ProductViewModel>();

        public static Product FromView(this ProductViewModel model) => model is null ? null : new Product
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            ImageUrl = model.ImageUrl,
            //Brand = model.Brand is null ? null : new Brand { Name = model.Name },
            BrandId = model.BrandId,
            SectionId = model.SectionId,
        };
    }
}
