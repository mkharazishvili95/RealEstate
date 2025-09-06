using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.SetAsMain
{
    public class SetAsMainImageHandler : IRequestHandler<SetAsMainImageRequest, SetAsMainImageResponse>
    {
        readonly IIdentityService _identityService;
        readonly ApplicationDbContext _db;
        public SetAsMainImageHandler(IIdentityService identityService, ApplicationDbContext db)
        {
            _identityService = identityService;
            _db = db;
        }

        public async Task<SetAsMainImageResponse> Handle(SetAsMainImageRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return new SetAsMainImageResponse { StatusCode = 400, Success = false, UserMessage = "Request is required." };

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return new SetAsMainImageResponse { StatusCode = 404, Success = false, UserMessage = "User not found." };

            var apartment = await _db.Apartments.FirstOrDefaultAsync(x =>
                x.ApartmentId == request.ApartmentId &&
                x.Status == Common.Enums.Apartment.ApartmentStatus.Active &&
                x.UserId == request.UserId);

            if (apartment == null)
                return new SetAsMainImageResponse { StatusCode = 404, Success = false, UserMessage = "Apartment not found." };

            var image = await _db.Files.FirstOrDefaultAsync(x =>
                x.Id == request.ImageId &&
                x.UserId == request.UserId &&
                !x.IsDeleted &&
                x.ApartmentId == request.ApartmentId);

            if (image == null)
                return new SetAsMainImageResponse { StatusCode = 404, Success = false, UserMessage = "Image not found." };

            if (image.MainImage == true)
                return new SetAsMainImageResponse { StatusCode = 400, Success = false, UserMessage = "Image is already set as main." };

            var oldMainImage = await _db.Files.FirstOrDefaultAsync(x =>
                x.ApartmentId == request.ApartmentId &&
                x.UserId == request.UserId &&
                !x.IsDeleted &&
                x.MainImage == true &&
                x.Id != request.ImageId);

            if (oldMainImage != null)
                oldMainImage.MainImage = false;

            image.MainImage = true;

            await _db.SaveChangesAsync();

            return new SetAsMainImageResponse
            {
                StatusCode = 200,
                Success = true,
                UserMessage = "Image set as main successfully."
            };
        }
    }
}
