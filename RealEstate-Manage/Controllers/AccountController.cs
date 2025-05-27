using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AuthApiService _authApiService;

    public AccountController(AuthApiService authApiService)
    {
        _authApiService = authApiService;
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

        HttpContext.Session.SetString("JWT", token);

        return RedirectToAction("Index", "Home");
    }
}
