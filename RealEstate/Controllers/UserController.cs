using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Apartment.FavoriteApartment;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("favorite-apartments")]
        public async Task<FavoriteApartmentsResponse> GetFavoriteApartments(FavoriteApartmenstRequest request) => await _mediator.Send(request);
    }
}
