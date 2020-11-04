using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Constants;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<CustomIdentityUser> _providerFactory;
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(
            ILogger<HomeController> logger,
            UserManager<CustomIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<CustomIdentityUser> providerFactory,
            SignInManager<CustomIdentityUser> signInManager)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
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
                        if (!await this._roleManager.RoleExistsAsync(UserRole.Admin))
                        {
                            await this._roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                        }

                        if (!await this._roleManager.RoleExistsAsync(UserRole.SuperAdmin))
                        {
                            await this._roleManager.CreateAsync(new IdentityRole(UserRole.SuperAdmin));
                        }

                        await this._userManager.AddToRoleAsync(user, UserRole.Admin);

                        var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmEmailUrl = Url.Action(
                                                    "ConfirmEmailAddress",
                                                    "Account",
                                                    new { Token = token, Email = viewModel.UserName },
                                                    Request.Scheme);

                        System.IO.File.WriteAllText("confirmEmailUrl.txt", confirmEmailUrl);

                        return View("Success");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
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

                if (user != null && !await this._userManager.IsLockedOutAsync(user))
                {
                    if (await this._userManager.CheckPasswordAsync(user, viewModel.Password))
                    {
                        if (!await this._userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "Email is not confirmed");
                            return View();
                        }

                        await this._userManager.ResetAccessFailedCountAsync(user);

                        // if having a 2SV requirement
                        if (await this._userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validProviders = await this._userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validProviders.Contains(this._userManager.Options.Tokens.AuthenticatorTokenProvider))
                            {
                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(user.Id, this._userManager.Options.Tokens.AuthenticatorTokenProvider));
                                return RedirectToAction("TwoFactor", "Account");
                            }

                            if (validProviders.Contains("Email"))
                            {
                                var token = await this._userManager.GenerateTwoFactorTokenAsync(user, "Email");
                                System.IO.File.WriteAllText("email2SA.txt", token);

                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(user.Id, "Email"));

                                return RedirectToAction("TwoFactor", "Account");
                            }
                        }

                        var principal = await this._providerFactory.CreateAsync(user);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                        return RedirectToAction("Index", "Home");

                        // var signInResult = await this._signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);

                        // if (signInResult.Succeeded)
                        // {
                        //     return RedirectToAction("Index", "Home");
                        // }
                    }

                    await this._userManager.AccessFailedAsync(user);

                    if (await this._userManager.IsLockedOutAsync(user))
                    {
                        // email user, notifying them of lockout
                    }
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
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

                    if (await this._userManager.IsLockedOutAsync(user))
                    {
                        await this._userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
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

        private ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }

        public IActionResult TwoFactor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactor(TwoFactorViewModel viewModel)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "You login request has expired, please start over");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));

                if (user != null)
                {
                    var isValid = await this._userManager.VerifyTwoFactorTokenAsync(
                            user, result.Principal.FindFirstValue("amr"), viewModel.Token);

                    if (isValid)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        var principal = await this._providerFactory.CreateAsync(user);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Invalid token");
                    return View();
                }

                ModelState.AddModelError("", "Invalid request");
            }

            return View();
        }

        public async Task<IActionResult> RegisterAuthenticator()
        {
            var user = await this._userManager.GetUserAsync(User);

            var authenticatorKey = await this._userManager.GetAuthenticatorKeyAsync(user);
            if (authenticatorKey == null)
            {
                await this._userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await this._userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new RegisterAuthenticatorViewModel { AuthenticatorKey = authenticatorKey });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAuthenticator(RegisterAuthenticatorViewModel viewModel)
        {
            var user = await this._userManager.GetUserAsync(User);

            var isValid = await this._userManager.VerifyTwoFactorTokenAsync(user, this._userManager.Options.Tokens.AuthenticatorTokenProvider, viewModel.Code);

            if (!isValid)
            {
                ModelState.AddModelError("", "Code is invalid");
                return View(viewModel);
            }

            await this._userManager.SetTwoFactorEnabledAsync(user, true);

            return View("Success");
        }

        public IActionResult ExternalLogin(string provider)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("ExternalLoginCallback", "Account"),
                Items = { { "scheme", provider } }
            };

            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            var externalUserId = result.Principal.FindFirstValue("sub")
                                ?? result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)
                                ?? throw new Exception("Cannot find external user id");

            var provider = result.Properties.Items["scheme"];

            var user = await this._userManager.FindByLoginAsync(provider, externalUserId);

            if (user == null)
            {
                var email = result.Principal.FindFirstValue("email")
                            ?? result.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    user = await this._userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new CustomIdentityUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = email,
                            Email = email
                        };

                        await this._userManager.CreateAsync(user);
                    }

                    await this._userManager.AddLoginAsync(user, new UserLoginInfo(provider, externalUserId, provider));
                }
            }

            if (user == null) return View("Error", "Home");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var claimsPrincipal = await this._providerFactory.CreateAsync(user);
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}