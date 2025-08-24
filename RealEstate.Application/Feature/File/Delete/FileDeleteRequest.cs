namespace RealEstate.Application.Feature.File.Delete
{
    public class FileDeleteRequest : IRequest<FileDeleteResponse>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
    }
}
