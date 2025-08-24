using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadHandler : IRequestHandler<FileUploadRequest, FileUploadResponse>
    {
        private readonly ApplicationDbContext _db;

        public FileUploadHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<FileUploadResponse> Handle(FileUploadRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FileContent) || string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentException("FileName and FileContent must be provided");

            string base64Content = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.FileContent));

            var fileEntity = new RealEstate.Core.File.File
            {
                ApartmentId = request.ApartmentId,
                FileName = request.FileName,
                FileContent = base64Content,
                FileType = request.FileType
            };

            _db.Files.Add(fileEntity);
            await _db.SaveChangesAsync(cancellationToken);

            return new FileUploadResponse
            {
                Id = fileEntity.Id,
                ApartmentId = fileEntity.ApartmentId,
                FileName = fileEntity.FileName, 
                FileUrl = null,
                FileType = fileEntity.FileType,
                Success = true,
                StatusCode = 200,
                UserMessage = "File uploaded successfully"
            };
        }
    }
}
