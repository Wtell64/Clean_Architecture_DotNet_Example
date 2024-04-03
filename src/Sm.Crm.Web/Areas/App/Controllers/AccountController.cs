using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models.Account;
using System.Net.Mail;
using System.Security.Claims;

namespace Sm.Crm.Web.Areas.App.Controllers;

[Area("App")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(AuthenticationRequest request, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var user = await _accountService.AuthenticateAsync(request);
            if (user != null)
            {
                return LocalRedirect(returnUrl == null ? "/App" : returnUrl);
            }
        }

        ModelState.AddModelError(string.Empty, "Username or Password is wrong!");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return LocalRedirect("/App");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (ModelState.IsValid)
        {
            var user = await _accountService.RegisterAsync(request);
            if (user != null)
            {
                ViewBag.Message = "User registered. Please activate your account in email.";
                return View();
            }
        }

        ModelState.AddModelError(string.Empty, "Please fill the form correctly!");
        return View();
    }

    public async Task<IActionResult> Activate(string userId, string code)
    {
        var isActivated = await _accountService.ActivateUserAsync(userId, code);
        if (isActivated)
        {
            TempData["Message"] = "Email confirmed. Please login.";
            return RedirectToAction("Login");
        }
        return Redirect("/");
    }

    public IActionResult AccessDenied() => View();

    // Google'a gidip Authentication yapan kısım
    public async Task GoogleLogin(string? returnUrl = null)
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new()
        {
            // Giriş yaptıktan sonra tekrar bizim uygulamamızda bu Action'a dönüş yapması için bu adresi tanımlıyoruz.
            RedirectUri = Url.Action("GoogleResponse", new { ReturnUrl = returnUrl })
        });
    }

    // Google'dan gelen bilgiler
    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (result.Succeeded)
        {
            var principal = result.Principal;
            var identity = principal.Identities.FirstOrDefault();
            var claims = identity?.Claims.ToList();

            var photoUrl = principal.FindFirstValue("urn:google:picture");
            var email = principal.FindFirstValue(ClaimTypes.Email);
            var name = principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = principal.FindFirstValue(ClaimTypes.Surname);
            var password = Guid.NewGuid().ToString(); // google ile giriş yaptığımız için rastgele bir değer atadık

            var loginRequest = new AuthenticationRequest
            {
                Email = email,
                Password = password,
                RememberMe = true,
                IsExternalAuthentication = true
            };

            var isUserExist = await _accountService.IsUserExist(email);
            if (!isUserExist)
            {
                var registerRequest = new RegisterRequest
                {
                    Email = email,
                    Password = password,
                    FirstName = name,
                    LastName = surname
                };
                var registeredUser = await _accountService.RegisterAsync(registerRequest);
                if (registeredUser != null)
                {
                    var authenticatedUser = await _accountService.AuthenticateAsync(loginRequest);
                    if (authenticatedUser != null)
                    {
                        return LocalRedirect(returnUrl == null ? "/App" : returnUrl);
                    }

                    return Redirect(returnUrl);
                }
            }
            else
            {
                var authenticatedUser = await _accountService.AuthenticateAsync(loginRequest);
                if (authenticatedUser != null)
                {
                    return LocalRedirect(returnUrl == null ? "/App" : returnUrl);
                }
            }

            return Redirect(returnUrl);
        }

        return RedirectToAction("Login");
    }
}