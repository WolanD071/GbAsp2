using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Interfaces;
using GbWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    /// <summary> products management </summary>
    [Route(WebApiRoutes.ProductsAPI)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productData;

        public ProductsApiController(IProductService productData) => _productData = productData;

        #region methods from IAnyEntityCRUD<>

        [NonAction]
        public IEnumerable<Product> Get() => _productData.Get();

        [NonAction]
        public Product Get(int id) => _productData.Get(id);

        [NonAction]
        public int Add(Product emp) => _productData.Add(emp);

        [NonAction]
        public void Update(Product emp) => _productData.Update(emp);

        [NonAction]
        public bool Delete(int id) => _productData.Delete(id);

        #endregion

        /// <summary> getting all the sections list from database </summary>
        /// <returns> list of sections </returns>
        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _productData.GetSections();

        /// <summary> getting specified section by its id </summary>
        /// <param name="id"> id </param>
        /// <returns> section found </returns>
        [HttpGet("sections/{id}")]
        public SectionDTO GetSectionById(int id) => _productData.GetSectionById(id);

        /// <summary> getting all the brands list from database </summary>
        /// <returns> list of brands </returns>
        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _productData.GetBrands();

        /// <summary> getting specified brand by its id </summary>
        /// <param name="id"> id </param>
        /// <returns> brand found </returns>
        [HttpGet("brands/{id}")]
        public BrandDTO GetBrandById(int id) => _productData.GetBrandById(id);

        /// <summary> getting the products list from database that satisfies some criteria (optional) </summary>
        /// <param name="Filter"> complicated object - filter (default is null) </param>
        /// <returns> list of products </returns>
        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => _productData.GetProducts(Filter);

        /// <summary> getting the product by its id </summary>
        /// <param name="id"> id </param>
        /// <returns> product found </returns>
        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _productData.GetProductById(id);
    }
}
