using RealEstate.Common.Enums.User;

namespace RealEstate.Core.User
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PIN { get; set; }
        public string? UserName { get; set; }
        public UserType Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public decimal? Balance { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockDate { get; set; } = DateTime.Now;
        public string? BlockReason { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
