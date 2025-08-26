using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Common.Enums.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly AuthApiService _authApiService;
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(AuthApiService authApiService, IHttpClientFactory httpClientFactory)
    {
        _authApiService = authApiService;
        _httpClientFactory = httpClientFactory;
    }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (model.RegistrationMethod == RegistrationMethod.Email && string.IsNullOrWhiteSpace(model.Email))
            ModelState.AddModelError(nameof(model.Email), "Email is required for email registration.");

        if (model.RegistrationMethod == RegistrationMethod.Phone && string.IsNullOrWhiteSpace(model.PhoneNumber))
            ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number is required for phone registration.");

        if (!ModelState.IsValid)
            return View(model);

        var client = _httpClientFactory.CreateClient();

        var response = await client.PostAsJsonAsync("https://localhost:5001/api/auth/register", model);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Login");
        }

        var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return View(model);
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var token = await _authApiService.LoginAsync(model.UserName, model.Password);
        if (token == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var role = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.Role || c.Type == "role" ||
            c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            ModelState.AddModelError("", "UserId not found in token.");
            return View(model);
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim("sub", userId),
                new Claim(ClaimTypes.Role, role ?? ""),
                new Claim("JWT", token) 
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return role == "Admin"
            ? RedirectToAction("Index", "Dashboard")
            : RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
