using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.Models.Auth
{
    public class UserLoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
