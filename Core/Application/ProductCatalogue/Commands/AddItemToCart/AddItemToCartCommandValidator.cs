using FluentValidation;
using ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart
{
    public class UpdateCartItemCommandValidator : AbstractValidator<AddItemToCartCommand>
    {
        public UpdateCartItemCommandValidator()
        {
            RuleFor(i => i.ProductId)
                .NotEmpty();
            RuleFor(i => i.Count)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
