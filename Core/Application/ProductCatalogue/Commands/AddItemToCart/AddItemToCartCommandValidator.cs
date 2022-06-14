using FluentValidation;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart
{
    public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
    {
        public AddItemToCartCommandValidator()
        {
            RuleFor(i => i.ProductId)
                .NotEmpty();
            RuleFor(i => i.Count)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
