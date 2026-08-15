using FluentValidation;
using AphelionBackend.DTOs.Cart;

namespace AphelionBackend.Validators.Cart;

public class CartRequestDtoValidator : AbstractValidator<CartRequest>
{
  public CartRequestDtoValidator()
  {
    RuleFor(x => x.GameId).ValidId();
    RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be 0 or greater (0 removes the item).");
  }
}