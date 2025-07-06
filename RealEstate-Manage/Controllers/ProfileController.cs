using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Profile.Agencies;
using RealEstate.Application.Feature.Profile.Apartments;
using RealEstate.Common.Enums.Agency;
using RealEstate.Common.Enums.Apartment;
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

        public async Task<IActionResult> MyApartments(ApartmentStatus? status)
        {
            var userId = User?.FindFirst("sub")?.Value;

            var requestUrl = $"{_baseUrl}/profile/my-apartments";

            var response = await _httpClient.PostAsJsonAsync(
                requestUrl,
                new MyApartmentsRequestModel
                {
                    UserId = userId,
                    Status = status
                });

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Could not load your apartments.";
                return View(new List<MyApartmentViewModel>());
            }

            var apiResult = await response.Content.ReadFromJsonAsync<MyApartmentsResponse>();

            if (apiResult == null || !apiResult.Success)
            {
                ViewBag.Error = apiResult?.UserMessage ?? "Error";
                return View(new List<MyApartmentViewModel>());
            }

            var apartments = apiResult.Items.Select(x => new MyApartmentViewModel
            {
                ApartmentId = x.ApartmentId,
                Title = x.Title ?? null,
                Description = x.Description ?? null,
                Status = (int?)x.Status,
                BlockReason = x.BlockReason ?? null,
                CreateDate = x.CreateDate ?? null,
                EndDate = x.EndDate ?? null,
                UpdateDate = x.UpdateDate ?? null,
                DeleteDate = x.DeleteDate ?? null,
                Price = x.Price ?? null,
                CurrencyId = x.CurrencyId ?? null,
                AgencyId = x.AgencyId ?? null,
                AgencyName = x.AgencyName ?? null
            }).ToList();

            return View(apartments);
        }

        [Authorize]
        public async Task<IActionResult> MyAgencies(string? agencyName, string? identificationNumber, AgencyType? type)
        {
            var userId = User?.FindFirst("sub")?.Value;

            var requestUrl = $"{_baseUrl}/profile/my-agencies";


            var response = await _httpClient.PostAsJsonAsync(
                requestUrl,
                new MyAgenciesRequestModel
                {
                   UserId = userId,
                   AgencyName = agencyName,
                   IdentificationNumber = identificationNumber,
                   Type = type
                });

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "საგენტოები ვერ მოიძებნა.";
                return View(new List<MyAgenciesViewModel>());
            }

            var apiResult = await response.Content.ReadFromJsonAsync<MyAgenciesResponse>();

            if (apiResult == null || !apiResult.Success)
            {
                ViewBag.Error = apiResult?.UserMessage ?? "მოხდა შეცდომა.";
                return View(new List<MyAgenciesViewModel>());
            }

            var agencies = apiResult.Items.Select(x => new MyAgenciesViewModel
            {
                AgencyId = x.AgencyId,
                AgencyType = x.AgencyType,
                Name = x.Name,
                LogoUrl = x.LogoUrl,
                IsApproved = x.IsApproved,
                IsDeleted = x.IsDeleted,
                DeleteReason = x.DeleteReason,
                UpdateDate = x.UpdateDate,
                DeleteDate = x.DeleteDate,
                Address = x.Address,
                IdentificationNumber = x.IdentificationNumber,
                Description = x.Description,
                Email = x.Email,
                Link = x.Link,
                PhoneNumber = x.PhoneNumber,
                CreateDate = x.CreateDate
            }).ToList();

            return View(agencies);
        }

        [Authorize]
        public async Task<IActionResult> Main()
        {
            var userId = User?.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userId))
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

            if (apiResponse == null || !apiResponse.Success)
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