using System.Linq;
using GbWebApp.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GbWebApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GbWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly UserManager<User> __userManager;
        readonly SignInManager<User> __signInManager;
        readonly ILogger<AccountController> __logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            __userManager = UserManager;
            __signInManager = SignInManager;
            __logger = Logger;
        }

        [AllowAnonymous]
        public ActionResult Register() => View(new RegisterViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            __logger.LogInformation($"Registration of {Model.UserName}...");

            var user = new User { UserName = Model.UserName };

            var regResult = await __userManager.CreateAsync(user, Model.Password);
            if (regResult.Succeeded)
            {
                __logger.LogInformation($"{Model.UserName} registered successfully!");
                await __userManager.AddToRoleAsync(user, Role.Users);
                __logger.LogInformation($"{Model.UserName} has gained the '{Role.Users}' role!");
                await __signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            __logger.LogWarning("Some error(s) occured during the {0} registration! Details: {1}",
                Model.UserName, string.Join(',', regResult.Errors.Select(e => e.Description)));

            foreach (var error in regResult.Errors)
                ModelState.AddModelError("", error.Description);

            return View(Model);
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await __signInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else 
                true
#endif
                );

            if (login_result.Succeeded)
                return LocalRedirect(Model.ReturnUrl ?? "/");

            ModelState.AddModelError("", "WRONG UserName and/or Password!");

            return View(Model);
        }

        public async Task<IActionResult> Logout()
        {
            await __signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}
