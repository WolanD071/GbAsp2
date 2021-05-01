using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.Entities;

namespace GbWebApp.Interfaces.Services
{
    public interface IProductData : IAnyEntityCRUD<Product>
    {
        IQueryable<Section> GetSections();

        IQueryable<Brand> GetBrands();

        IQueryable<Product> GetProducts(ProductFilter Filter = null);

        Product GetProductById(int id);
    }
}
