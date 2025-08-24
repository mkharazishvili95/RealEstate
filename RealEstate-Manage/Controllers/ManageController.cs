using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Models.User;
using RealEstate.Common.Enums.User;
using RealEstate.Models.User.List;
using RealEstate_Manage.Controllers;
using RealEstate_Manage.Extensions;
using RealEstate_Manage.Helpers;
using RealEstate_Manage.Models.Agency;
using RealEstate_Manage.Models.Agency.List;
using RealEstate_Manage.Models.Apartment;
using RealEstate_Manage.Models.Apartment.List;
using RealEstate_Manage.Models.User;
using RealEstate_Manage.Models.User.List;
using System.Net.Http.Headers;
using System.Text;

namespace RealEstate.MVC.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    public class ManageController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly MapToTableRowsHelper _mapper;
        private readonly string _baseUrl;

        public ManageController(IHttpClientFactory httpClientFactory, MapToTableRowsHelper mapper, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _mapper = mapper;
            /* Local, ჩემი აპის შემთხვევაში: https://localhost:7010/api */
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList(UserFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }

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

            var requestUrl = $"{_baseUrl}/Manage/users";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Cannot load users at this time.");
                return View(new UserListViewModel { Filter = filter });
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<GetUserListForManageResponse>();

            var tableRows = apiResponse?.UserListForManage != null
                ? _mapper.MapToTableRowsForUsers(apiResponse.UserListForManage).ToList()
                : new List<UserTableRowViewModel>();

            var viewModel = new UserListViewModel
            {
                Filter = filter,
                Users = tableRows,
                TotalCount = apiResponse?.TotalCount ?? 0
            };

            return View(viewModel);
        }

        [HttpGet("Manage/ExportUsersToCsv")]
        public async Task<IActionResult> ExportUsersCsv(UserFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }

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

            var requestUrl = $"{_baseUrl}/Manage/users";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "CSV export failed.";
                return RedirectToAction("UserList", filter);
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<GetUserListForManageResponse>();

            if (apiResponse?.UserListForManage == null || !apiResponse.UserListForManage.Any())
            {
                TempData["Error"] = "No users found for export.";
                return RedirectToAction("UserList", filter);
            }

            var csvBytes = ExportCsvHelper.GenerateUserCsv(apiResponse.UserListForManage);
            var fileName = $"Users_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            return File(csvBytes, "text/csv", fileName);
        }

        public async Task<IActionResult> AgencyList(AgencyFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
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

            var requestUrl = $"{_baseUrl}/Manage/agencies";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);
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

        [HttpGet("Manage/ExportAgenciesToCsv")]
        public async Task<IActionResult> ExportAgenciesCsv(AgencyFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
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

            var requestUrl = $"{_baseUrl}/Manage/agencies";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "CSV export failed.";
                return RedirectToAction("AgencyList", filter);
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<GetAgencyListForManageResponse>();

            if (apiResponse?.AgencyListForManage == null || !apiResponse.AgencyListForManage.Any())
            {
                TempData["Error"] = "No agencies found for export.";
                return RedirectToAction("AgencyList", filter);
            }

            var csvBytes = ExportCsvHelper.GenerateAgencyCsv(apiResponse.AgencyListForManage);
            var fileName = $"Agencies_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            return File(csvBytes, "text/csv", fileName);
        }

        public async Task<IActionResult> ApartmentList(ApartmentFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            SetApartmentStatusSelectList();
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

            var requestUrl = $"{_baseUrl}/Manage/apartments";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);

            var apiResponse = await response.Content.ReadFromJsonAsync<GetApartmentListForManageResponse>();

            var tableRows = new List<ApartmentTableRowViewModel>();

            if (apiResponse?.ApartmentListForManage != null)
            {
                tableRows = new MapToTableRowsHelper()
                    .MapToTableRowsForApartments(apiResponse.ApartmentListForManage)
                    .ToList();
            }

            ViewBag.ApiBaseUrl = _baseUrl;

            return View(new ApartmentListViewModel
            {
                Filter = filter,
                Apartments = tableRows,
                TotalCount = apiResponse.TotalCount
            });
        }

        [HttpGet("Manage/ExportApartmentsToCsv")]
        public async Task<IActionResult> ExportApartmentsCsv(ApartmentFilterRowModel filter)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
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

            var requestUrl = $"{_baseUrl}/Manage/apartments";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(apiRequest)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "CSV export failed.";
                return RedirectToAction("ApartmentList", filter);
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<GetApartmentListForManageResponse>();

            if (apiResponse?.ApartmentListForManage == null || !apiResponse.ApartmentListForManage.Any())
            {
                TempData["Error"] = "No apartments found for export.";
                return RedirectToAction("ApartmentList", filter);
            }

            var csvBytes = ExportCsvHelper.GenerateApartmentCsv(apiResponse.ApartmentListForManage);
            var fileName = $"Apartments_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            return File(csvBytes, "text/csv", fileName);
        }

        [HttpPut]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }

            var requestUrl = $"{_baseUrl}/Manage/block-user";

            var httpRequest = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = JsonContent.Create(new BlockUserRequestModel
                {
                    UserId = request.UserId,
                    BlockReason = request.BlockReason
                })
            };
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "მომხმარებელი წარმატებით დაიბლოკა" });
            }
            else
            {
                return Json(new { success = false, message = "მომხმარებლის დაბლოკვისას მოხდა შეცდომა" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UnblockUser([FromBody] UnblockUserRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }

            var requestUrl = $"{_baseUrl}/Manage/unblock-user";

            var httpRequest = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = JsonContent.Create(new UnblockUserRequestModel
                {
                    UserId = request.UserId,
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "მომხმარებელი წარმატებით განიბლოკა" });
            }
            else
            {
                return Json(new { success = false, message = "მომხმარებლის განბლოკვისას მოხდა შეცდომა." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> TopUpBalance([FromBody] TopUpBalanceRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            if (request.UserId == null)
                return Json(new { success = false, message = "მომხმარებლის ბალანსის შევსებისას მოხდა შეცდომა." });

            if (request.Balance <= 0)
                return Json(new { success = false, message = "შეყვანილი ბალანსი უნდა იყოს 0-ზე მეტი" });

            var requestUrl = $"{_baseUrl}/Manage/top-up-balance";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(new TopUpBalanceRequest
                {
                    UserId = request.UserId,
                    Balance = request.Balance
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "მომხმარებლის ბალანსი წარმატებით შეივსო" });
            }
            else
            {
                return Json(new { success = false, message = "მომხმარებლის ბალანსის შევსებისას მოხდა შეცდომა." });
            }
        }

        [HttpPut]
        public async Task<IActionResult> BlockApartment([FromBody] BlockApartmentRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            var requestUrl = $"{_baseUrl}/Manage/block-apartment";

            var httpRequest = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = JsonContent.Create(new BlockApartmentRequestModel
                {
                    ApartmentId = request.ApartmentId,
                    BlockReason = request.BlockReason
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "უძრავი ქონება წარმატებით დაიბლოკა" });
            }
            else
            {
                return Json(new { success = false, message = "უძრავი ქონების დაბლოკვისას მოხდა შეცდომა" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UnblockApartment([FromBody] UnblockApartmentRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            var requestUrl = $"{_baseUrl}/Manage/unblock-apartment";

            var httpRequest = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = JsonContent.Create(new BlockApartmentRequestModel
                {
                    ApartmentId = request.ApartmentId
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "უძრავი ქონება წარმატებით განიბლოკა" });
            }
            else
            {
                return Json(new { success = false, message = "უძრავი ქონების განბლოკვისას მოხდა შეცდომა." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAgency([FromBody] ApproveAgencyRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            var requestUrl = $"{_baseUrl}/Manage/approve-agency";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(new ApproveAgencyRequestModel
                {
                    AgencyId = request.AgencyId
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "სააგენტო წარმატებით დადასტურდა" });
            }
            else
            {
                return Json(new { success = false, message = "სააგენტოს წაშლისას მოხდა შეცდომა." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAgency([FromBody]DeleteAgencyRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            var requestUrl = $"{_baseUrl}/Manage/delete-agency";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(new DeleteAgencyRequestModel
                {
                    AgencyId = request.AgencyId,
                    DeleteReason = request.DeleteReason
                })
            };

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "სააგენტო წარმატებით წაიშალა" });
            }
            else
            {
                return Json(new { success = false, message = "სააგენტოს წაშლისას მოხდა შეცდომა." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreAgency([FromBody] RestoreAgencyRequestModel request)
        {
            string token;
            try
            {
                token = GetToken();
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            var requestUrl = $"{_baseUrl}/Manage/restore-agency";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = JsonContent.Create(new RestoreAgencyRequestModel
                {
                    AgencyId = request.AgencyId
                })
            };
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "სააგენტო წარმატებით განიბლოკა" });
            }
            else
            {
                return Json(new { success = false, message = "სააგენტოს განბლოკვისას მოხდა შეცდომა." });
            }
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
        private void SetApartmentStatusSelectList()
        {
            ViewBag.Status = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "აირჩიეთ სტატუსი" },
                new SelectListItem { Value = "0", Text = "აქტიური" },
                new SelectListItem { Value = "1", Text = "ვადაგასული" },
                new SelectListItem { Value = "2", Text = "დაბლოკილი" },
                new SelectListItem { Value = "3", Text = "წაშლილი" }
            };
        }
    }
}
