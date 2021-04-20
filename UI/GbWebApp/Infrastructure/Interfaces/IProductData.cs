using GbWebApp.Domain;
using GbWebApp.Domain.Entities;
using System.Linq;

namespace GbWebApp.Infrastructure.Interfaces
{
    public interface IProductData : IAnyEntityCRUD<Product>
    {
        IQueryable<Section> GetSections();

        IQueryable<Brand> GetBrands();

        IQueryable<Product> GetProducts(ProductFilter Filter = null);

        Product GetProductById(int id);
    }
}
