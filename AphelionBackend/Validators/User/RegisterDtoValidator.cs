using FluentValidation;
using AphelionBackend.DTOs.User;

namespace AphelionBackend.Validators.User;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username).ValidUsername();
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Password).StrongPassword();
    }
}