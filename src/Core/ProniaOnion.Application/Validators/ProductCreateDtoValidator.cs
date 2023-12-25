using FluentValidation;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Validators
{
    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is important").MaximumLength(100).WithMessage("Name must contain max 100 characters").MinimumLength(2).WithMessage("Name must may contain min 2 characters");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("Description must contain max 1000 characters");
            RuleFor(x => x.SKU).NotEmpty().MaximumLength(10).WithMessage("Name must contain max 10 characters ");
            RuleFor(x => x.Price).NotEmpty().LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10);
            RuleFor(x => x.CategoryId).Must(c => c> 0);
            RuleForEach(x => x.ColorIds).Must(c => c > 0);
            RuleFor(x => x.ColorIds).NotEmpty();

        }
    }
}
