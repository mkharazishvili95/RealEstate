namespace RealEstate.Application.Feature.Profile.Transfer
{
    public class TransferBalanceRequest : IRequest<TransferBalanceResponse>
    {
        public string? SenderUserId { get; set; }
        public string? ReceiverPIN { get; set; }
        public decimal? Amount { get; set; }
    }
}
