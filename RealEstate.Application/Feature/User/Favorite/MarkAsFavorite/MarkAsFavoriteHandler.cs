using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.User.Favorite.MarkAsFavorite
{
    public class MarkAsFavoriteHandler : IRequestHandler<MarkAsFavoriteRequest, MarkAsFavoriteResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        public MarkAsFavoriteHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<MarkAsFavoriteResponse> Handle(MarkAsFavoriteRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return new MarkAsFavoriteResponse { StatusCode = 400, Success = false, UserMessage = "Request is required" };

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return new MarkAsFavoriteResponse { StatusCode = 404, Success = false, UserMessage = "User not found" };

            var apartment = await _db.Apartments.FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId && x.Status == Common.Enums.Apartment.ApartmentStatus.Active && x.UserId != user.UserId);
            if (apartment == null)
                return new MarkAsFavoriteResponse { StatusCode = 404, Success = false, UserMessage = "Apartment not found" };

            var alreadyFavorite = await _db.UserFavoriteApartments.FirstOrDefaultAsync(x => x.UserId == user.UserId && x.ApartmentId == apartment.ApartmentId);

            if (alreadyFavorite != null)
            {
                alreadyFavorite.Comment = request.Comment;
                alreadyFavorite.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                var favoriteApartment = new RealEstate.Core.User.UserFavoriteApartment
                {
                    UserId = user.UserId,
                    ApartmentId = apartment.ApartmentId,
                    Comment = request.Comment,
                    UpdateDate = DateTime.UtcNow
                };
                await _db.UserFavoriteApartments.AddAsync(favoriteApartment);
            }
            await _db.SaveChangesAsync();
            return new MarkAsFavoriteResponse
            {
                StatusCode = 200,
                Success = true,
                UserMessage = alreadyFavorite != null
            ? "Comment saved successfully"
            : "Apartment added to favorites successfully"
            };
        }
    }
}
