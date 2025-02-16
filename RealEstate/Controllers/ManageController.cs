using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.User;
using RealEstate.Application.Services;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/Manage")]
    public class ManageController : ControllerBase
    {
        private readonly IManageService _manageService;
        private readonly IMediator _mediator;
        public ManageController(IManageService manageService, IMediator mediator)
        {
            _manageService = manageService;
            _mediator = mediator;
        }

        [HttpPost("get-user-list-for-manage")]
        public async Task<GetUserListForManageResponse> GetUserListForManage([FromBody] GetUserListForManageRequest request) => await _mediator.Send(request);

        [HttpPost("get-agency-list-for-manage")]
        public async Task<GetAgencyListForManageResponse> GetAgencyListForManage([FromBody] GetAgencyListForManageRequest request) => await _mediator.Send(request);

        [HttpPost("get-apartment-list-for-manage")]
        public async Task<GetApartmentListForManageResponse> GetApartmentListForManage([FromBody] GetApartmentListForManageRequest request) => await _mediator.Send(request);

        [HttpPut("block-user")]
        public async Task<UserBlockResponseModel> BlockUser(string userId) => await _manageService.BlockUser(userId);

        [HttpPut("unblock-user")]
        public async Task<UserUnBlockResponseModel> UnBlockUser(string userId) => await _manageService.UnBlockUser(userId);

        [HttpPost("top-up-balance")]
        public async Task<TopUpBalanceResponse> UnBlockUser(string userId, decimal balance) => await _manageService.TopUpBalance(userId, balance);

        [HttpDelete("delete-agency")]
        public async Task<AgencyDeleteResponseModel> DeleteAgency(int agencyId, string? deleteReason) => await _manageService.DeleteAgency(agencyId, deleteReason);

        [HttpPost("restore-agency")]
        public async Task<AgencyRestoreResponseModel> RestoreAgency(int agencyId) => await _manageService.RestoreAgency(agencyId);

    }
}
