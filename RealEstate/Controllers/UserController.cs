using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Apartment.FavoriteApartment;
using RealEstate.Application.Feature.User.Apartment.Get;
using RealEstate.Application.Feature.User.Favorite.MarkAsFavorite;
using RealEstate.Application.Feature.User.Favorite.RemoveFromFavorites;

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

        [HttpPost("user-apartments")]
        public async Task<GetUserApartmentsResponse> GetUserApartments(GetUserApartmentsRequest request) => await _mediator.Send(request);

        [HttpPut("mark-as-favorite")]
        public async Task<MarkAsFavoriteResponse> MarkAsFavorite(MarkAsFavoriteRequest request) => await _mediator.Send(request);

        [HttpDelete("remove-from-favorites")]
        public async Task<RemoveFromFavoritesResponse> RemoveFromFavorites(RemoveFromFavoritesRequest request) => await _mediator.Send(request);
    }
}
