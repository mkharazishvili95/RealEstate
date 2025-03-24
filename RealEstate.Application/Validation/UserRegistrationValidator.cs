using FluentValidation;
using RealEstate.Application.Models.Auth;
using RealEstate.Application.Services;

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

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Must(email => !_userService.EmailExists(email)).WithMessage("Email already exists.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Must(phone => !_userService.PhoneNumberExists(phone)).WithMessage("Phone number already exists.");
    }
}