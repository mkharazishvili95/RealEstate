using FluentValidation;
using RealEstate.Application.Models.Auth;
using RealEstate.Application.Services;
using RealEstate.Common.Enums.Auth;
using System.Text.RegularExpressions;

public class UserRegistrationValidator : AbstractValidator<UserRegisterModel>
{
    private readonly IUserService _userService;

    public UserRegistrationValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.RegistrationMethod)
            .IsInEnum().WithMessage("Registration method is invalid.");

        When(x => x.RegistrationMethod == RegistrationMethod.Email, () =>
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Must(email => !_userService.EmailExists(email)).WithMessage("Email already exists.");
        });

        When(x => x.RegistrationMethod == RegistrationMethod.Phone, () =>
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(IsValidPhoneNumber).WithMessage("Invalid phone number format.")
                .Must(phone => !_userService.PhoneNumberExists(phone)).WithMessage("Phone number already exists.");
        });
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\+?\d{7,15}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}