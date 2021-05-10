using System.Collections.Generic;
using System.Linq;
using GbWebApp.DAL.Context;
using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Services.Services.InDB
{
    public class InDbProductData : InDbAnyEntity<Product>, IProductService
    {
        private readonly GbWebAppDB __db;
        private readonly ILogger _logger;

        public InDbProductData(GbWebAppDB db, ILogger<InDbProductData> logger) : base(db, logger)
        {
            __db = db;
            _logger = logger;
        }

        public IEnumerable<SectionDTO> GetSections() => __db.Sections.Include(s => s.Products).ToDTO();

        public SectionDTO GetSectionById(int id) =>
            __db.Sections.Include(s => s.Products).FirstOrDefault(s => s.Id == id).ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => __db.Brands.Include(b => b.Products).ToDTO();

        public BrandDTO GetBrandById(int id) =>
            __db.Brands.Include(b => b.Products).FirstOrDefault(b => b.Id == id).ToDTO();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IQueryable<Product> query = __db.Products.Include(p => p.Section).Include(p => p.Brand);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);
                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }
            return query.AsEnumerable().ToDTO();
        }

        public ProductDTO GetProductById(int id) => __db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id)
            .ToDTO();
    }
}
