using System.ComponentModel.DataAnnotations;

namespace RealEstate_Manage.Models.Profile
{
    public class TransferBalanceViewModel
    {
        public string SenderUserId { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითეთ მიმღების PIN")]
        public string ReceiverPIN { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითეთ თანხა")]
        [Range(0.01, double.MaxValue, ErrorMessage = "თანხა უნდა იყოს 0.01-ზე მეტი")]
        public decimal Amount { get; set; }
    }
}
