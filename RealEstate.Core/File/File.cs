using RealEstate.Common.Enums.File;

namespace RealEstate.Core.File
{
    public class File
    {
        public int Id { get; set; }
        public int? ApartmentId { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
        public FileType? FileType { get; set; }
    }
}
