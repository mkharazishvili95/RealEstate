namespace RealEstate.Application.Feature.File.SetAsMain
{
    public class SetAsMainImageRequest : IRequest<SetAsMainImageResponse>
    {
        public string UserId { get; set; }
        public int ImageId { get; set; }
        public int ApartmentId { get; set; }
    }
}
