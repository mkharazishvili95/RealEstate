using Microsoft.Extensions.Configuration;
using RealEstate.Common.Enums.File;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadHandler : IRequestHandler<FileUploadRequest, FileUploadResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly string _uploadsFolder;

        public FileUploadHandler(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            //ფოტოების Folder-ის მისამართი ჩემ შემთხვევაში:
            _uploadsFolder = configuration["FileStorage:UploadPath"];
            if (string.IsNullOrWhiteSpace(_uploadsFolder))
                throw new ArgumentException("Upload path is not configured.");

            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);
        }

        public async Task<FileUploadResponse> Handle(FileUploadRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FileContent) || string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentException("FileName and FileContent must be provided");

            byte[] fileBytes = Convert.FromBase64String(request.FileContent);

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
                        _ => ".bin"
                    };
                    request.FileContent = parts[1];
                }
            }
            string uniqueFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_uploadsFolder, uniqueFileName);

            await System.IO.File.WriteAllBytesAsync(filePath, fileBytes, cancellationToken);

            string fileUrl = $"/uploads/apartments/{uniqueFileName}";

            var fileEntity = new RealEstate.Core.File.File
            {
                ApartmentId = request.ApartmentId,
                FileName = request.FileName,
                FileUrl = fileUrl,
                FileType = request.FileType
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
