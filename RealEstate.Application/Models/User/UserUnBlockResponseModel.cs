namespace RealEstate.Application.Models.User
{
    public class UserUnBlockResponseModel : ResponseBaseModel { }
    public class UseUnBlockRequest : IRequest<UserUnBlockResponseModel>
    {
        public string? UserId { get; set; }
    }
}
