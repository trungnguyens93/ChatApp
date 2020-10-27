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
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(
            ILogger<HomeController> logger,
            UserManager<CustomIdentityUser> userManager,
            IUserClaimsPrincipalFactory<CustomIdentityUser> providerFactory,
            SignInManager<CustomIdentityUser> signInManager)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._providerFactory = providerFactory;
            this._signInManager = signInManager;
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
                        var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmEmailUrl = Url.Action(
                                                    "ConfirmEmailAddress",
                                                    "Account",
                                                    new { Token = token, Email = viewModel.UserName },
                                                    Request.Scheme);

                        System.IO.File.WriteAllText("confirmEmailUrl.txt", confirmEmailUrl);

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
                    if (!await this._userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Email is not confirmed");
                        return View();
                    }

                    var principal = await this._providerFactory.CreateAsync(user);
                    await HttpContext.SignInAsync("Identity.Application", principal);
                    return RedirectToAction("Index", "Home");

                    // var signInResult = await this._signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);

                    // if (signInResult.Succeeded)
                    // {
                    //     return RedirectToAction("Index", "Home");
                    // }
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(viewModel.Email);

                if (user != null)
                {
                    var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Account", new { Token = token, Email = viewModel.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("resetUrl.txt", resetUrl);
                }
                else
                {

                }

                return View("Success");
            }

            return View();
        }

        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(viewModel.Email);

                if (user != null)
                {
                    var reset = await this._userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);

                    if (!reset.Succeeded)
                    {
                        foreach (var error in reset.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
                    }

                    return View("Success");
                }

                ModelState.AddModelError("", "Invalid Request");
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await this._userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Success");
                }
            }

            return View("Error", "Home");
        }
    }
}