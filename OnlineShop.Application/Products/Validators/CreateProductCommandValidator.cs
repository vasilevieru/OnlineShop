using FluentValidation;
using OnlineShop.Application.Products.Commands;

namespace OnlineShop.Application.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Category).MaximumLength(30).NotEmpty();
            RuleFor(x => x.SubCategory).MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
