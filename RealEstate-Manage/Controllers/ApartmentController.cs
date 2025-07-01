using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.Feature.Apartment.Create;
using RealEstate.Application.Feature.Profile.Agencies;
using RealEstate_Manage.Models.Agency;
using RealEstate_Manage.Models.Apartment.Create;

namespace RealEstate_Manage.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApartmentController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient();
            _baseUrl = config["ApiSettings:BaseUrl"];
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User?.FindFirst("sub")?.Value;
            var viewModel = new CreateApartmentViewModel
            {
                UserId = userId,
                Agencies = await GetUserAgenciesAsync(new GetMyAgenciesRequestModel { UserId = userId })
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApartmentViewModel model)
        {
            var userId = User?.FindFirst("sub")?.Value;

            if (!ModelState.IsValid)
                return View(model);

            var request = new CreateApartmentRequest
            {
                Title = model.Title,
                Price = model.Price,
                CurrencyId = model.CurrencyId,
                ApartmentType = model.ApartmentType,
                ApartmentDealType = model.ApartmentDealType,
                ApartmentState = model.ApartmentState,
                BuildingStatus = model.BuildingStatus,
                CityId = model.CityId,
                DistrictId = model.DistrictId,
                SubdistrictId = model.SubdistrictId,
                StreetId = model.StreetId,
                Description = model.Description,
                UserId = userId,
                AgencyId = model.AgencyId
            };

            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/apartment/create", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error: {error}");
                return View(model);
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<CreateApartmentResponse>();

            TempData["Success"] = apiResponse?.UserMessage;
            return RedirectToAction("List");
        }
        private async Task<List<SelectListItem>> GetUserAgenciesAsync(GetMyAgenciesRequestModel request)
        {
            var agencies = new List<SelectListItem>();

            if (string.IsNullOrWhiteSpace(request.UserId))
                return agencies;

            var requestUrl = $"{_baseUrl}/profile/my-agencies";

            var response = await _httpClient.PostAsJsonAsync(requestUrl, request);

            if (!response.IsSuccessStatusCode)
                return agencies;

            var result = await response.Content.ReadFromJsonAsync<MyAgenciesResponse>();

            if (result?.Items == null) return agencies;

            agencies = result.Items
                .Select(a => new SelectListItem
                {
                    Value = a.AgencyId.ToString(),
                    Text = a.Name
                }).ToList();

            return agencies;
        }
    }
}
