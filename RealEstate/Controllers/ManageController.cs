using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.Agent.ChangeStatus.AgentIntoIndividual;
using RealEstate.Application.Feature.Manage.Agent.ChangeStatus.IndividualIntoAgent;
using RealEstate.Application.Feature.Manage.Agent.PotentialAgent.Get;
using RealEstate.Application.Feature.Manage.Apartment.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.Apartment;
using RealEstate.Application.Models.User;
using RealEstate.Application.Services;
using RealEstate.Common.Enums.User;

namespace RealEstate.Controllers
{
    [ApiController]
    [Authorize(Roles = nameof(UserType.Admin))]
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
        public async Task<TopUpBalanceResponse> TopUpBalance(TopUpBalanceRequest request) => await _manageService.TopUpBalance(request);

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
        
        [HttpPost("potential-agents")]
        public async Task<GetPotentialAgentListResponse> GetPotentialAgents([FromBody] GetPotentialAgentListRequest request) => await _mediator.Send(request);

        [HttpPut("change-agent-to-individual")]
        public async Task<ChangeAgentToIndividualResponse> ChangeAgentToIndividual(ChangeAgentToIndividualRequest request) => await _mediator.Send(request);

        [HttpPut("change-individual-to-agent")]
        public async Task<ChangeIndividualToAgentResponse> ChangeIndividualToAgent(ChangeIndividualToAgentRequest request) => await _mediator.Send(request);

    }
}
