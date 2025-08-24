using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.Delete
{
    public class FileDeleteHandler : IRequestHandler<FileDeleteRequest, FileDeleteResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        public FileDeleteHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<FileDeleteResponse> Handle(FileDeleteRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return new FileDeleteResponse { Success = false, StatusCode = 400, UserMessage = "UserId should not be empty." };

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return new FileDeleteResponse { Success = false, StatusCode = 404, UserMessage = "User not found." };

            var file = await _db.Files.FirstOrDefaultAsync(x => x.Id == request.Id 
            && x.UserId == user.UserId 
            || user.Type == Common.Enums.User.UserType.Admin);

            if(file == null)
                return new FileDeleteResponse { Success = false, StatusCode = 404, UserMessage = "File not found." };

            if(file.IsDeleted)
                return new FileDeleteResponse { Success = false, StatusCode = 400, UserMessage = "File is already deleted." };

            file.IsDeleted = true;
            file.DeleteDate = DateTime.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);
            return new FileDeleteResponse
            {
                Success = true,
                StatusCode = 200,
                UserMessage = "File deleted successfully"
            };
        }
    }
}
