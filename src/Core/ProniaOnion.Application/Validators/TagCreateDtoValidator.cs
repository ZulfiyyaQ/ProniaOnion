using FluentValidation;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.Validators
{
    public class TagCreateDtoValidator : AbstractValidator<TagCreateDto>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(X => X.Name).NotEmpty().MaximumLength(100).MinimumLength(2);
        }
        
    }
}
