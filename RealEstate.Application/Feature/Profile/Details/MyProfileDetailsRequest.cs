namespace RealEstate.Application.Feature.Profile.Details
{
    public class MyProfileDetailsRequest : IRequest<MyProfileDetailsResponse>
    {
        public string UserId { get; set; }
    }
}
