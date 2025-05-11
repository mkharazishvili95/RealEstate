namespace RealEstate.Application.Models.User
{
    public class UserBlockResponseModel : ResponseBaseModel { }
    public class UserBlockRequest : IRequest<UserBlockResponseModel>
    {
        public string? UserId { get; set; }
        public string? BlockReason { get; set; }
    }
}
