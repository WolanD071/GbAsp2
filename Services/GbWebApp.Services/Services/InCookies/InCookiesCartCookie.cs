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
    public class InCookiesCartCookie : ICartCookie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        public Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;
                var cartCookies = context.Request.Cookies[_cartName];
                if (cartCookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookies(cookies, cartCookies);
                return JsonConvert.DeserializeObject<Cart>(cartCookies);
            }
            set => ReplaceCookies(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie);
        }

        public InCookiesCartCookie(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = httpContextAccessor.HttpContext!.User;
            var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;
            _cartName = $"GbWebAppCart{userName}";
        }
    }
}
