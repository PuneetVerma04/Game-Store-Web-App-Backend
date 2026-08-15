using FluentValidation;
using AphelionBackend.DTOs.User;

namespace AphelionBackend.Validators.User;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).ValidEmail();

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}