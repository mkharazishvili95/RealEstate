using RealEstate.Application.Models;
using RealEstate.Common.Enums.File;

namespace RealEstate.Application.Feature.File.Upload
{
    public class FileUploadResponse : ResponseBaseModel
    {
        public int Id { get; set; }
        public int? ApartmentId { get; set; }
        public string? FileName { get; set; }
        public string? FileContent { get; set; }
        public FileType? FileType { get; set; }
    }
}
