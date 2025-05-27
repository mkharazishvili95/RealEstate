using System.ComponentModel.DataAnnotations;
using RealEstate.Common.Enums.Auth;
using RealEstate.Common.Enums.User;

public class RegisterViewModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public Gender? Gender { get; set; }

    [Required]
    public RegistrationMethod RegistrationMethod { get; set; }
}
