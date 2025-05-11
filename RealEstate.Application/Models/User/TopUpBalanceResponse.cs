namespace RealEstate.Application.Models.User
{
    public class TopUpBalanceResponse : ResponseBaseModel { }
    public class TopUpBalanceRequest : IRequest<TopUpBalanceResponse>
    {
        public string? UserId { get; set; }
        public decimal? Balance { get; set; }
    }
}
