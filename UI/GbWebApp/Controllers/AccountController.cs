using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GbWebApp.Domain.Entities.Identity;
using GbWebApp.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GbWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Register() => View(new RegisterViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _logger.LogInformation($"Registration of {model.UserName}...");

            using (_logger.BeginScope($"*** REGISTRATION OF '{model.UserName}' SCOPE ***"))
            {
                var user = new User { UserName = model.UserName };
                var regResult = await _userManager.CreateAsync(user, model.Password);
                if (regResult.Succeeded)
                {
                    _logger.LogInformation($"{model.UserName} registered successfully!");
                    await _userManager.AddToRoleAsync(user, Role.Users);
                    _logger.LogInformation($"{model.UserName} has gained the '{Role.Users}' role!");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                _logger.LogWarning("Some error(s) occurred during the {0} registration! Details: {1}",
                    model.UserName, string.Join(',', regResult.Errors.Select(e => e.Description)));
                foreach (var error in regResult.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var loginResult = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
#if DEBUG
                false
#else 
                true
#endif
                );

            if (loginResult.Succeeded)
                return LocalRedirect(model.ReturnUrl ?? "/");

            ModelState.AddModelError("", "WRONG UserName and/or Password!");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
