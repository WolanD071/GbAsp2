using GbWebApp.DAL.Context;
using GbWebApp.Domain;
using GbWebApp.Domain.Entities;
using System.Linq;
using GbWebApp.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GbWebApp.Infrastructure.Services.InDB
{
    public class InDbProductData : InDbAnyEntity<Product>, IProductData
    {
        readonly GbWebAppDB __db;

        public InDbProductData(GbWebAppDB db) : base(db) { __db = db; }

        public IQueryable<Section> GetSections() => __db.Sections.Include(s => s.Products);

        public IQueryable<Brand> GetBrands() => __db.Brands.Include(b => b.Products);

        public IQueryable<Product> GetProducts(ProductFilter Filter)
        {
            IQueryable<Product> query = __db.Products; // 'var' keyword is not allowed! if we want have ability to make queries

            if (Filter?.Ids?.Length > 0)
            {
                query = query.Where(product => Filter.Ids.Contains(product.Id)).AsQueryable();
            }
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id).AsQueryable();
                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id).AsQueryable();
            }
            return query;
        }

        public Product GetProductById(int id) => __db.Products.
            Include(p => p.Brand).Include(p => p.Section).FirstOrDefault(p => p.Id == id);
    }
}
