using System;
using System.Linq;
using System.Net.Http;
using GbWebApp.Domain;
using GbWebApp.Interfaces;
using GbWebApp.Domain.DTO;
using GbWebApp.Clients.Base;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace GbWebApp.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApiRoutes.ProductsAPI) { }

        #region methods from IAnyEntityCRUD<>

        public IEnumerable<Product> Get() => throw new NotImplementedException();

        public Product Get(int id) => throw new NotImplementedException();

        public int Add(Product emp) => throw new NotImplementedException();

        public void Update(Product emp) => throw new NotImplementedException();

        public bool Delete(int id) => throw new NotImplementedException();

        #endregion

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            Post(Address, Filter ?? new ProductFilter()).Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");
    }
}
