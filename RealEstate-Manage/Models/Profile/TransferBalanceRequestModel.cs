namespace RealEstate_Manage.Models.Profile
{
    public class TransferBalanceRequestModel
    {
        public string SenderUserId { get; set; }
        public string ReceiverPIN { get; set; }
        public decimal Amount { get; set; }
    }
}
