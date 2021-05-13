using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.Entities;
using GbWebApp.Services.Mappers;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartCookie _cartCookie;
        private readonly IProductService _productData;

        public CartService(ICartCookie cartCookie, IProductService productData)
        {
            _cartCookie = cartCookie;
            _productData = productData;
        }

        public CartViewModel GetViewModel()
        {
            var cart = _cartCookie.Cart;
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = cart.Items.Select(item => item.ProductId).ToArray()
            }).FromDTO();
            var productViewModels = products.ToView().ToDictionary(p => p.Id);
            return new CartViewModel
            {
                Items = cart.Items.Where(item => productViewModels.ContainsKey(item.ProductId))
                    .Select(item => (productViewModels[item.ProductId], item.Quantity))
            };
        }

        public void Clear()
        {
            var cart = _cartCookie.Cart;
            cart.Items.Clear();
            _cartCookie.Cart = cart;
        }

        public void Increment(int id)
        {
            var cart = _cartCookie.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) cart.Items.Add(new CartItem { ProductId = id });
            else item.Quantity++;
            _cartCookie.Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = _cartCookie.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            if (item.Quantity > 1) item.Quantity--;
            else cart.Items.Remove(item);
            _cartCookie.Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = _cartCookie.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);
            _cartCookie.Cart = cart;
        }
    }
}
