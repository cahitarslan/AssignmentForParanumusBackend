using Business.Utilities.Consts;
using Entities.Concrete;
using FluentValidation;

namespace Business.Utilities.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage(ValidationMessages.Product.NameNotEmpty);
        RuleFor(p => p.Name).MinimumLength(2).WithMessage(ValidationMessages.Product.NameMinimumLength);
        RuleFor(p => p.Name).MaximumLength(100).WithMessage(ValidationMessages.Product.NameMaximumLength);

        RuleFor(p => p.Description).MaximumLength(1000).WithMessage(ValidationMessages.Product.DescriptionMaximumLength);

        RuleFor(p => p.Price).NotEmpty().WithMessage(ValidationMessages.Product.PriceNotEmpty);
        RuleFor(p => p.Price).GreaterThan(0).WithMessage(ValidationMessages.Product.PriceGreaterThan);
    }
}
