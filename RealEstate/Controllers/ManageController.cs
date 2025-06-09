using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.Export.Agency;
using RealEstate.Application.Feature.Manage.Export.Apartment;
using RealEstate.Application.Feature.Manage.Export.User;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.Apartment;
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

        [HttpPost("approve-agency")]
        public async Task<AgencyApproveResponseModel> ApproveAgency(AgencyApproveRequest request) => await _manageService.ApproveAgency(request);

        [HttpPost("delete-agency")]
        public async Task<AgencyDeleteResponseModel> DeleteAgency(AgencyDeleteRequest request) => await _manageService.DeleteAgency(request);

        [HttpPost("restore-agency")]
        public async Task<AgencyRestoreResponseModel> RestoreAgency(AgencyRestoreRequest request) => await _manageService.RestoreAgency(request);

        [HttpPut("block-apartment")]
        public async Task<BlockApartmentResponse> BlockApartment(BlockApartmentRequest request) => await _manageService.BlockApartment(request);

        [HttpPut("unblock-apartment")]
        public async Task<UnblockApartmentResponse> UnblockApartment(UnblockApartmentRequest request) => await _manageService.UnblockApartment(request);

        [HttpGet("export-users")]
        public async Task<IActionResult> ExportUsers([FromQuery] GetUserListForManageRequest filters)
        {
            var result = await _mediator.Send(new ExportUserListCsvRequest { Filter = filters });

            return File(result.FileContent, result.ContentType, result.FileName);
        }

        [HttpGet("export-agencies")]
        public async Task<IActionResult> ExportAgencies([FromQuery] GetAgencyListForManageRequest filters)
        {
            var result = await _mediator.Send(new ExportAgencyListCsvRequest { Filter = filters });

            return File(result.FileContent, result.ContentType, result.FileName);
        }

        [HttpGet("export-apartments")]
        public async Task<IActionResult> ExportApartments([FromQuery] GetApartmentListForManageRequest filter)
        {
            var result = await _mediator.Send(new ExportApartmentListCsvRequest { Filter = filter });

            return File(result.FileContent, result.ContentType, result.FileName);
        }
    }
}
