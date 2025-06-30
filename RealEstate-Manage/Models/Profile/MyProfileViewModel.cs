using RealEstate.Common.Enums.User;

namespace RealEstate_Manage.Models.Profile
{
    public class MyProfileViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PIN { get; set; }
        public string? UserName { get; set; }
        public UserType? Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? BlockDate { get; set; }
        public string? BlockReason { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
    }

    public class MyProfileApiResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? UserMessage { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PIN { get; set; }
        public string? UserName { get; set; }
        public RealEstate.Common.Enums.User.UserType? Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? BlockDate { get; set; }
        public string? BlockReason { get; set; }
        public RealEstate.Common.Enums.User.Gender? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
