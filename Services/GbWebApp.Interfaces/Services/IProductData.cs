using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;

namespace GbWebApp.Interfaces.Services
{
    public interface IProductData : IAnyEntityCRUD<Product>
    {
        IQueryable<SectionDTO> GetSections();

        IQueryable<BrandDTO> GetBrands();

        IQueryable<ProductDTO> GetProducts(ProductFilter Filter = null);

        ProductDTO GetProductById(int id);
    }
}
