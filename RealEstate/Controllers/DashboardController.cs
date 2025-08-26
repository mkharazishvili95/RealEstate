using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Dashboard.Statistic.Agency;
using RealEstate.Application.Feature.Dashboard.Statistic.Apartment;
using RealEstate.Application.Feature.Dashboard.Statistic.User;
using RealEstate.Common.Enums.User;

namespace RealEstate.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    [Authorize(Roles = nameof(UserType.Admin))]
    public class DashboardController : ControllerBase
    {
        readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("apartments-statistic-count")]
        public async Task<GetApartmentsStatisticCountResponse> GetApartmentsStatisticCount(GetApartmentsStatisticCountRequest request) => await _mediator.Send(request);

        [HttpPost("agencies-statistic-count")]
        public async Task<GetAgenciesStatisticCountResponse> GetAgenciesStatisticCount(GetAgenciesStatisticCountRequest request) => await _mediator.Send(request);

        [HttpPost("users-statistic-count")]
        public async Task<GetUsersStatisticCountResponse> GetUsersStatisticCount(GetUsersStatisticCountRequest request) => await _mediator.Send(request);
    }
}
