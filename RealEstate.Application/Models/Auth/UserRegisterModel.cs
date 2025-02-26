using System.ComponentModel.DataAnnotations;
using RealEstate.Common.Enums.User;

namespace RealEstate.Application.Models.Auth
{
    public class UserRegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
    }
}
