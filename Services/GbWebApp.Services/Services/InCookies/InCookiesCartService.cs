using System.Linq;
using GbWebApp.Domain;
using GbWebApp.Domain.Entities;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Mappers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GbWebApp.Services.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor __httpContextAccessor;
        private readonly IProductData __productData;
        private readonly string __cartName;

        private Cart Cart
        {
            get
            {
                var context = __httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;
                var cart_cookies = context.Request.Cookies[__cartName];
                if (cart_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(__cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookies(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set => ReplaceCookies(__httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(__cartName);
            cookies.Append(__cartName, cookie);
        }

        public InCookiesCartService(IHttpContextAccessor HttpContextAccessor, IProductData ProductData)
        {
            __httpContextAccessor = HttpContextAccessor;
            __productData = ProductData;
            var user = HttpContextAccessor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;
            __cartName = $"GbWebAppCart{user_name}";
        }

        public CartViewModel GetViewModel()
        {
            var products = __productData.GetProducts(new ProductFilter
            { Ids = Cart.Items.Select(item => item.ProductId).ToArray() });
            var product_view_models = products.ToView().ToDictionary(p => p.Id);
            return new CartViewModel
            {
                Items = Cart.Items.Where(item => product_view_models.ContainsKey(item.ProductId))
                   .Select(item => (product_view_models[item.ProductId], item.Quantity))
            };
        }

        public void Clear()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void Increment(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id });
            else
                item.Quantity++;
            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            if (item.Quantity > 1)
                item.Quantity--;
            else
                cart.Items.Remove(item);
            Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            cart.Items.Remove(item);
            Cart = cart;
        }
    }
}
