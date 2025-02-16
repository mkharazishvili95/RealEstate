using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Models.User.List;
using RealEstate_Manage.Helpers;

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
                PageSize = filter.PageSize,
                Page = filter.Page
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7010/api/Manage/get-user-list-for-manage", apiRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetUserListForManageResponse>();

            var mapper = new MapToTableRowsHelper();
            var tableRows = mapper.MapToTableRows(apiResponse.UserListForManage).ToList();
            return View(tableRows);

        }
    }
}
