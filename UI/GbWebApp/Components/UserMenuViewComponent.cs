using Microsoft.AspNetCore.Mvc;

namespace GbWebApp.Components
{
    public class UserMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity?.IsAuthenticated == true ?
            View("UserMenu") : View();
    }
}
