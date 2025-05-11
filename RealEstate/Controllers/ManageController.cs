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

        [HttpPost("users")]
        public async Task<GetUserListForManageResponse> GetUserListForManage([FromBody] GetUserListForManageRequest request) => await _mediator.Send(request);

        [HttpPost("agencies")]
        public async Task<GetAgencyListForManageResponse> GetAgencyListForManage([FromBody] GetAgencyListForManageRequest request) => await _mediator.Send(request);

        [HttpPost("apartments")]
        public async Task<GetApartmentListForManageResponse> GetApartmentListForManage([FromBody] GetApartmentListForManageRequest request) => await _mediator.Send(request);

        [HttpPut("block-user")]
        public async Task<UserBlockResponseModel> BlockUser(UserBlockRequest request) => await _manageService.BlockUser(request);

        [HttpPut("unblock-user")]
        public async Task<UserUnBlockResponseModel> UnBlockUser(UseUnBlockRequest request) => await _manageService.UnBlockUser(request);

        [HttpPost("top-up-balance")]
        public async Task<TopUpBalanceResponse> UnBlockUser(TopUpBalanceRequest request) => await _manageService.TopUpBalance(request);

        [HttpDelete("delete-agency")]
        public async Task<AgencyDeleteResponseModel> DeleteAgency(int agencyId, string? deleteReason) => await _manageService.DeleteAgency(agencyId, deleteReason);

        [HttpPost("restore-agency")]
        public async Task<AgencyRestoreResponseModel> RestoreAgency(int agencyId) => await _manageService.RestoreAgency(agencyId);

    }
}
