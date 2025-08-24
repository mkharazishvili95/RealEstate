using RealEstate.Common.Enums.File;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadRequest : IRequest<FileUploadResponse>
    {
        public int? ApartmentId { get; set; }
        public string? FileName { get; set; }
        public string? FileContent { get; set; }
        public FileType? FileType { get; set; }
        public string? UserId { get; set; }
    }
}
