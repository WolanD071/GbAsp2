using System;
using System.Net.Http;
using GbWebApp.Domain;
using GbWebApp.Interfaces;
using GbWebApp.Domain.DTO;
using GbWebApp.Clients.Base;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        private readonly ILogger<ProductsClient> _logger;

        public ProductsClient(IConfiguration cfg, ILogger<ProductsClient> logger) : base(cfg, WebApiRoutes.ProductsAPI) => _logger = logger;

        #region methods from IAnyEntityCRUD<>

        public IEnumerable<Product> Get() => throw new NotImplementedException();

        public Product Get(int id) => throw new NotImplementedException();

        public int Add(Product product)
        {
            _logger.LogInformation("Creating new product...");
            using (_logger.BeginScope("*** CREATING PRODUCT SCOPE ***"))
            {
                var result = Post($"{Address}/newproduct", product).Content.ReadAsAsync<int>().Result;
                _logger.LogInformation($"...completed successfully! id={result}");
                return result;
            }
        }

        public void Update(Product product)
        {
            _logger.LogInformation($"Updating the product with id={product.Id}...");
            using (_logger.BeginScope("*** UPDATING PRODUCT SCOPE ***"))
            {
                Put(Address, product);
                _logger.LogInformation("...completed successfully!");
            }
        }

        public bool Delete(int id)
        {
            _logger.LogInformation($"Deleting the product with id={id}...");
            using (_logger.BeginScope("*** DELETING PRODUCT SCOPE ***"))
            {
                var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
                _logger.LogInformation("{0}", result ? "...completed successfully!" : "product not found!");
                return result;
            }
        }

        #endregion

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null) =>
            Post(Address, filter ?? new ProductFilter()).Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");
    }
}
