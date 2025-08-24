using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadHandler : IRequestHandler<FileUploadRequest, FileUploadResponse>
    {
        readonly ApplicationDbContext _db;
        public FileUploadHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<FileUploadResponse> Handle(FileUploadRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
