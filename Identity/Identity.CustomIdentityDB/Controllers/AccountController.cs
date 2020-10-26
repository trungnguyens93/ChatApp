using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity.CustomIdentityDB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<CustomIdentityUser> _providerFactory;

        public AccountController(
            ILogger<HomeController> logger,
            UserManager<CustomIdentityUser> userManager,
            IUserClaimsPrincipalFactory<CustomIdentityUser> providerFactory)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._providerFactory = providerFactory;
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByNameAsync(viewModel.UserName);

                if (user == null)
                {
                    user = new CustomIdentityUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = viewModel.UserName,
                        Email = viewModel.UserName
                    };

                    var result = await this._userManager.CreateAsync(user, viewModel.Password);

                    if (result.Succeeded)
                    {
                        return View("Success");
                    }
                }
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewMode viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByNameAsync(viewModel.UserName);

                if (user != null && await this._userManager.CheckPasswordAsync(user, viewModel.Password))
                {
                    var principle = await this._providerFactory.CreateAsync(user);
                    await HttpContext.SignInAsync("Identity.Application", principle);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }
    }
}