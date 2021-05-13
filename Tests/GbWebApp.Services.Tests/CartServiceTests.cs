using Moq;
using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Services.Services;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assert = Xunit.Assert;

namespace GbWebApp.Services.Tests
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _cart;

        private Mock<IProductService> _productDataMock;
        private Mock<ICartCookie> _cartCookieMock;

        private ICartService _cartService;

        [TestInitialize]
        public void Initialize()
        {
            _cart = new Cart { Items = new List<CartItem> { new() { ProductId = 1, Quantity = 1 }, new() { ProductId = 2, Quantity = 3 } } };
            _productDataMock = new Mock<IProductService>();
            _productDataMock.Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(Enumerable.Range(1, 3).Select(i => new ProductDTO
               {
                   Id = i,
                   Name = $"Product {i}",
                   Price = (decimal)(i + (float)i / 10),
                   Order = i,
                   ImageUrl = $"Img_{i}.png",
                   Brand = new BrandDTO { Id = i, Name = $"Brand{i}", Order = i },
                   Section = new SectionDTO { Id = i, Name = $"Section{i}", Order = i }
               }));
            _cartCookieMock = new Mock<ICartCookie>();
            _cartCookieMock.Setup(c => c.Cart).Returns(_cart);
            _cartService = new CartService(_cartCookieMock.Object, _productDataMock.Object);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_returns_Correct_Quantity()
        {
            var cart = _cart;
            var expectedItemsCount = _cart.Items.Sum(i => i.Quantity);
            Assert.Equal(expectedItemsCount, cart.ItemsCount);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cartViewModel = new CartViewModel
            {
                Items = new[] { ( new ProductViewModel { Id = 1, Price = 0.5m }, 1 ), ( new ProductViewModel { Id = 2, Price = 1.5m }, 3 ) }
            };
            const int expectedCount = 4;

            Assert.Equal(expectedCount, cartViewModel.ItemsCount);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_TotalPrice()
        {
            var cartViewModel = new CartViewModel
            {
                Items = new[] { ( new ProductViewModel { Id = 1, Price = 0.5m }, 1 ), ( new ProductViewModel { Id = 2, Price = 1.5m }, 3 ) }
            };
            const int expectedTotalPrice = 5;

            Assert.Equal(expectedTotalPrice, cartViewModel.TotalPrice);
        }

        [TestMethod]
        public void CartService_Increment_Works_Correct()
        {
            const int expectedId = 5;
            const int expectedItemsCount = 1;

            _cart.Items.Clear();
            _cartService.Increment(expectedId);

            Assert.Equal(expectedItemsCount, _cart.ItemsCount);
            Assert.Single(_cart.Items);
            Assert.Equal(expectedId, _cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_Remove_Works_Correct()
        {
            const int itemId = 1;
            const int expectedProductId = 2;

            _cartService.Remove(itemId);

            Assert.Single(_cart.Items);
            Assert.Equal(expectedProductId, _cart.Items.Single().ProductId);
        }

        [TestMethod]
        public void CartService_Clear_ClearCart()
        {
            _cartService.Clear();

            Assert.Empty(_cart.Items);
        }

        [TestMethod]
        public void CartService_Decrement_Works_Correct()
        {
            const int itemId = 2;
            const int expectedQuantity = 2;
            const int expectedItemsCount = 3;
            const int expectedProductsCount = 2;

            _cartService.Decrement(itemId);

            Assert.Equal(expectedItemsCount, _cart.ItemsCount);
            Assert.Equal(expectedProductsCount, _cart.Items.Count);
            var items = _cart.Items.ToArray();
            Assert.Equal(itemId, items[1].ProductId);
            Assert.Equal(expectedQuantity, items[1].Quantity);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement_to_0()
        {
            const int itemId = 1;
            const int expectedItemsCount = 3;

            _cartService.Decrement(itemId);

            Assert.Equal(expectedItemsCount, _cart.ItemsCount);
            Assert.Single(_cart.Items);
        }

        [TestMethod]
        public void CartService_GetViewModel_Works_Correct()
        {
            const int expectedItemsCount = 4;
            const decimal expectedFirstProductPrice = 1.1m;

            var result = _cartService.GetViewModel();

            Assert.Equal(expectedItemsCount, result.ItemsCount);
            Assert.Equal(expectedFirstProductPrice, result.Items.First().Product.Price);
        }
    }
}
