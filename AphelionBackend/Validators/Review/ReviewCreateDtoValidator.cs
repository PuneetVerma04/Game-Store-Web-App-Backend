using FluentValidation;
using AphelionBackend.DTOs.Review;
using AphelionBackend.Validators;

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        // UserId and GameId are no longer in DTO - they come from JWT claims and route parameters

        RuleFor(x => x.Comment)
            .MaximumLength(500).WithMessage("Comment too long.")
            .When(x => x.Comment != null);

        RuleFor(x => x.Rating)
            .ValidRating();
    }
}
