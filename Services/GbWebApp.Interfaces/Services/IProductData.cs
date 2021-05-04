using System.Collections.Generic;
//using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;

namespace GbWebApp.Interfaces.Services
{
    public interface IProductData : IAnyEntityCRUD<Product>
    {
        IEnumerable<SectionDTO> GetSections();

        SectionDTO GetSectionById(int id);

        IEnumerable<BrandDTO> GetBrands();

        BrandDTO GetBrandById(int id);

        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);

        ProductDTO GetProductById(int id);
    }
}
