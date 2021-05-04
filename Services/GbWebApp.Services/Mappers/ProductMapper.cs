using System.Linq;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.ViewModels;
using System.Collections.Generic;

namespace GbWebApp.Services.Mappers
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
        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.ToDTO(),
            Section = product.Section.ToDTO(),
        };

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            BrandId = product.Brand?.Id,
            Brand = product.Brand.FromDTO(),
            SectionId = product.Section.Id,
            Section = product.Section.FromDTO(),
        };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(ToDTO);

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(FromDTO);
    }
}
