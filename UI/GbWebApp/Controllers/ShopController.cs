using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GbWebApp.Domain;
using System.Linq;
using GbWebApp.Domain.ViewModels;
using GbWebApp.Interfaces.Services;
using GbWebApp.Services.Mappers;
using GbWebApp.Domain.DTO;

namespace GbWebApp.Controllers
{
    public class ShopController : Controller
    {
        readonly IProductService __productData;
        readonly ICartService __cartData;
        int pageSize = 9;
        public ShopController(IProductService productData, ICartService cartData)
        {
            __productData = productData;
            __cartData = cartData;
        }
        public IActionResult Index(int? BrandId, int? SectionId, int prodPage = 1)
        {
            var filter = new ProductFilter { BrandId = BrandId, SectionId = SectionId };
            var products = __productData.GetProducts(filter).FromDTO();
            return View(new ShopViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products.OrderBy(p => p.Order).ToView().Skip((prodPage - 1) * pageSize).Take(pageSize),
                PagingInfo = new PaginationViewModel
                { CurrentPage = prodPage, ItemsPerPage = pageSize, TotalItems = products.Count() }
            });
        }
        public IActionResult ShopDetails(int id) => View(__productData.GetProductById(id).FromDTO().ToView());
        public IActionResult ShopCheckOut() => View();
        public IActionResult ShopCart() => View((__cartData.GetViewModel(), new OrderViewModel { }));
        public IActionResult AddToCart(int id)
        {
            __cartData.Increment(id);
            return RedirectToAction(nameof(ShopCart));
        }
        public IActionResult DecFromCart(int id)
        {
            __cartData.Decrement(id);
            return RedirectToAction(nameof(ShopCart));
        }
        public IActionResult RemoveFromCart(int id)
        {
            __cartData.Remove(id);
            return RedirectToAction(nameof(ShopCart));
        }
        public IActionResult ClearCart()
        {
            __cartData.Clear();
            return RedirectToAction(nameof(ShopCart));
        }
        [Authorize]
        public async Task<IActionResult> OrderCheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(ShopCart), (__cartData.GetViewModel(), orderModel));

            var order_model = new OrderModel
            {
                Order = orderModel,
                Items = __cartData.GetViewModel().Items.Select(item => new OrderItemDTO
                { Id = item.Product.Id, Price = item.Product.Price, Quantity = item.Quantity }).ToList()
            };

            var order = await orderService.CreateOrder(User.Identity!.Name, order_model);

            __cartData.Clear();
            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }
        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
        public IActionResult ShopLogin() => View();
    }
}
