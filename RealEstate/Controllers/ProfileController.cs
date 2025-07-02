using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Profile.Agencies;
using RealEstate.Application.Feature.Profile.Apartments;
using RealEstate.Application.Feature.Profile.Details;
using RealEstate.Application.Feature.Profile.Transfer;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/Profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("my-profile")]
        public async Task<MyProfileDetailsResponse> MyProfile([FromBody]MyProfileDetailsRequest request) => await _mediator.Send(request);

        [HttpPost("my-apartments")]
        public async Task<MyApartmentsResponse> MyApartments([FromBody]MyApartmentsRequest request) => await _mediator.Send(request);

        [HttpPost("my-agencies")]
        public async Task<MyAgenciesResponse> MyAgencies([FromBody]MyAgenciesRequest request) => await _mediator.Send(request);

        [HttpPut("transfer")]
        public async Task<TransferBalanceResponse> TransferBalance(TransferBalanceRequest request) => await _mediator.Send(request);
    }
}
