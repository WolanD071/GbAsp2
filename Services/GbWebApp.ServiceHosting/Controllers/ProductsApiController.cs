using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using GbWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    [Route(WebApiRoutes.ProductsAPI)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData) => _productData = productData;

        #region methods from IAnyEntityCRUD<>

        public IEnumerable<Product> Get() => _productData.Get();

        public Product Get(int id) => _productData.Get(id);

        public int Add(Product emp) => _productData.Add(emp);

        public void Update(Product emp) => _productData.Update(emp);

        public bool Delete(int id) => _productData.Delete(id);

        #endregion

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _productData.GetSections();

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _productData.GetBrands();

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => _productData.GetProducts(Filter);

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _productData.GetProductById(id);
    }
}
