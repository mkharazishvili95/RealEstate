using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Manage.Models.Profile;

namespace RealEstate_Manage.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _baseUrl;

        public ProfileController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            /* Local, ჩემი აპის შემთხვევაში: https://localhost:7010/api */
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            var userId = User?.FindFirst("sub")?.Value;

            Console.WriteLine($"IsAuthenticated: {isAuthenticated}, UserId: {userId}");

            if (!isAuthenticated || string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Account");

            var requestUrl = $"{_baseUrl}/Profile/my-profile";

            var response = await _httpClient.PostAsJsonAsync(
                requestUrl,
                new MyProfileRequestModel
                {
                    UserId = userId
                });

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var apiResponse = await response.Content.ReadFromJsonAsync<MyProfileApiResponse>();

            if (apiResponse is null || !apiResponse.Success)
                return View("Error");

            var model = new MyProfileViewModel
            {
                FirstName = apiResponse.FirstName,
                LastName = apiResponse.LastName,
                UserName = apiResponse.UserName,
                Balance = apiResponse.Balance,
                Email = apiResponse.Email,
                PhoneNumber = apiResponse.PhoneNumber,
                IsBlocked = apiResponse.IsBlocked,
                BlockDate = apiResponse.BlockDate,
                BlockReason = apiResponse.BlockReason,
                RegisterDate = apiResponse.RegisterDate,
                Type = apiResponse.Type,
                Gender = apiResponse.Gender,
                PIN = apiResponse.PIN
            };

            return View(model);
        }
    }
}