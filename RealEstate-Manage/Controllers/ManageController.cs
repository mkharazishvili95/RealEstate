using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Models.User.List;
using RealEstate_Manage.Extensions;
using RealEstate_Manage.Helpers;
using RealEstate_Manage.Models.Agency.List;
using RealEstate_Manage.Models.Apartment.List;
using RealEstate_Manage.Models.User.List;

namespace RealEstate.MVC.Controllers
{
    public class ManageController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly MapToTableRowsHelper _mapper;

        public ManageController(IHttpClientFactory httpClientFactory, MapToTableRowsHelper mapper)
        {
            _httpClient = httpClientFactory.CreateClient();
            _mapper = mapper;
        }

        public async Task<IActionResult> UserList(UserFilterRowModel filter)
        {
            var apiRequest = new GetUserListForManageRequest
            {
                UserId = filter.UserId,
                FirstName = filter.FirstName,
                LastName = filter.LastName,
                PIN = filter.PIN,
                UserName = filter.UserName,
                Type = filter.Type,
                Email = filter.Email,
                PhoneNumber = filter.PhoneNumber,
                BalanceFrom = filter.BalanceFrom,
                BalanceTo = filter.BalanceTo,
                IsBlocked = filter.IsBlocked,
                BlockDateFrom = filter.BlockDateFrom,
                BlockDateTo = filter.BlockDateTo,
                Gender = filter.Gender,
                RegisterDateFrom = filter.RegisterDateFrom,
                RegisterDateTo = filter.RegisterDateTo,
                PageSize = filter.PageSize,
                Page = filter.Page
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7010/api/Manage/users", apiRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetUserListForManageResponse>();

            var mapper = new MapToTableRowsHelper();
            var tableRows = new List<UserTableRowViewModel>();

            if (apiResponse?.UserListForManage != null)
            {
                tableRows = new MapToTableRowsHelper()
                    .MapToTableRowsForUsers(apiResponse.UserListForManage)
                    .ToList();
            }

            var viewModel = new UserListViewModel
            {
                Filter = filter,
                Users = tableRows,
                TotalCount = apiResponse.TotalCount
            };

            return View(viewModel);
        }


        public async Task<IActionResult> AgencyList(AgencyFilterRowModel filter)
        {
            var apiRequest = new GetAgencyListForManageRequest
            {
                AgencyId = filter.AgencyId,
                Name = filter.Name,
                AgencyType = filter.AgencyType,
                Email = filter.Email,
                IdentificationNumber = filter.IdentificationNumber,
                OwnerPIN = filter.OwnerPIN,
                PhoneNumber = filter.PhoneNumber,
                IsDeleted = filter.IsDeleted,
                IsApproved = filter.IsApproved,
                CreateDateFrom = filter.CreateDateFrom,
                CreateDateTo = filter.CreateDateTo,
                UpdateDateFrom = filter.UpdateDateFrom,
                UpdateDateTo = filter.UpdateDateTo,
                DeleteDateFrom = filter.DeleteDateFrom,
                DeleteDateTo = filter.DeleteDateTo,
                Page = filter.Page,
                PageSize = filter.PageSize
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7010/api/Manage/agencies", apiRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetAgencyListForManageResponse>();

            var mapper = new MapToTableRowsHelper();
            var tableRows = mapper.MapToTableRowsForAgencies(apiResponse.AgencyListForManage).ToList();

            var viewModel = new AgencyListViewModel
            {
                Filter = filter,
                Agencies = tableRows,
                TotalCount = apiResponse.TotalCount,
                AgencyTypeOptions = AgencyTypeExtensions.GetAgencyTypeOptions()
                    .Select(at => new SelectListItem
                    {
                        Text = at.Text,
                        Value = at.Value,
                        Selected = filter.AgencyType.HasValue && at.Value == filter.AgencyType.ToString()
                    })
                    .ToList()
            };

            viewModel.AgencyTypeOptions.Insert(0, new SelectListItem { Text = "ყველა", Value = "" });

            return View(viewModel);
        }

        public async Task<IActionResult> ApartmentList(ApartmentFilterRowModel filter)
        {
            SetCurrencySelectList();

            var apiRequest = new GetApartmentListForManageRequest
            {
                AgencyId = filter.AgencyId,
                ApartmentId = filter.ApartmentId,
                CreateDateFrom = filter.CreateDateFrom,
                CreateDateTo = filter.CreateDateTo,
                CurrencyId = filter.CurrencyId,
                DeleteDateFrom = filter.DeleteDateFrom,
                DeleteDateTo = filter.DeleteDateTo,
                PriceFrom = filter.PriceFrom,
                PriceTo = filter.PriceTo,
                Status = filter.Status,
                Title = filter.Title,
                UserId = filter.UserId,
                UserPin = filter.UserPin,
                UpdateDateFrom = filter.UpdateDateFrom,
                UpdateDateTo = filter.UpdateDateTo,
                PageSize = filter.PageSize ?? 20,
                Page = filter.Page ?? 1
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7010/api/Manage/apartments", apiRequest);

            var apiResponse = await response.Content.ReadFromJsonAsync<GetApartmentListForManageResponse>();

            var tableRows = new List<ApartmentTableRowViewModel>();

            if (apiResponse?.ApartmentListForManage != null)
            {
                tableRows = new MapToTableRowsHelper()
                    .MapToTableRowsForApartments(apiResponse.ApartmentListForManage)
                    .ToList();
            }

            return View(new ApartmentListViewModel
            {
                Filter = filter,
                Apartments = tableRows,
                TotalCount = apiResponse.TotalCount
            });
        }

        private void SetCurrencySelectList()
        {
            ViewBag.Currencies = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "აირჩიეთ ვალუტა" },
                new SelectListItem { Value = "1", Text = "ლარი" },
                new SelectListItem { Value = "2", Text = "დოლარი" }
            };
        }
    }
}
