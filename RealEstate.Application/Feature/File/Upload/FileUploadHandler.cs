using Imagekit.Sdk;
using Microsoft.Extensions.Options;
using RealEstate.Application.Configuration;
using RealEstate.Application.Services;
using RealEstate.Common.Enums.File;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadHandler : IRequestHandler<FileUploadRequest, FileUploadResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;
        private readonly ImagekitClient _imageKitClient;

        public FileUploadHandler(
            ApplicationDbContext db,
            IIdentityService identityService,
            IOptions<ImageKitSettings> imageKitOptions)
        {
            _db = db;
            _identityService = identityService;

            var settings = imageKitOptions.Value;
            _imageKitClient = new ImagekitClient(
                settings.PublicKey,
                settings.PrivateKey,
                settings.UrlEndpoint
            );
        }

        public async Task<FileUploadResponse> Handle(FileUploadRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return new FileUploadResponse
                {
                    Success = false,
                    StatusCode = 400,
                    UserMessage = "UserId should not be empty."
                };

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return new FileUploadResponse
                {
                    Success = false,
                    StatusCode = 404,
                    UserMessage = "User not found."
                };

            if (string.IsNullOrWhiteSpace(request.FileContent) || string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentException("FileName and FileContent must be provided");

            string base64Data = request.FileContent;
            string extension = request.FileType switch
            {
                FileType.Image => ".jpeg",
                FileType.Video => ".mp4",
                FileType.Document => ".pdf",
                _ => ".bin"
            };

            if (request.FileContent.StartsWith("data:"))
            {
                var parts = request.FileContent.Split(',', 2);
                if (parts.Length == 2)
                {
                    var mime = parts[0].Split(':', ';')[1];
                    extension = mime switch
                    {
                        "image/jpeg" => ".jpg",
                        "image/png" => ".png",
                        "image/gif" => ".gif",
                        "video/mp4" => ".mp4",
                        "application/pdf" => ".pdf",
                        _ => ".bin"
                    };
                    base64Data = parts[1];
                }
            }

            string uniqueFileName = $"{Guid.NewGuid()}{extension}";

            var uploadRequest = new FileCreateRequest
            {
                fileName = uniqueFileName,
                folder = "/apartments",
                useUniqueFileName = true,
                file = base64Data,
                checks = null,
                tags = null,
                customCoordinates = null,
                responseFields = null,
                isPrivateFile = false,
                overwriteFile = false,
                customMetadata = null,
                webhookUrl = null,
                overwriteTags = false,
                overwriteCustomMetadata = false
            };

            dynamic uploadResult;
            try
            {
                uploadResult = await _imageKitClient.UploadAsync(uploadRequest);
            }
            catch (Exception ex)
            {
                return new FileUploadResponse
                {
                    Success = false,
                    StatusCode = 500,
                    UserMessage = $"File upload failed: {ex.Message}"
                };
            }

            string fileUrl = uploadResult.url;

            var fileEntity = new RealEstate.Core.File.File
            {
                ApartmentId = request.ApartmentId,
                FileName = request.FileName,
                FileUrl = fileUrl,
                FileType = request.FileType,
                UploadDate = DateTime.UtcNow
            };

            _db.Files.Add(fileEntity);
            await _db.SaveChangesAsync(cancellationToken);

            return new FileUploadResponse
            {
                Id = fileEntity.Id,
                ApartmentId = fileEntity.ApartmentId,
                FileName = fileEntity.FileName,
                FileUrl = fileEntity.FileUrl,
                FileType = fileEntity.FileType,
                Success = true,
                StatusCode = 200,
                UserMessage = "File uploaded successfully"
            };
        }
    }
}