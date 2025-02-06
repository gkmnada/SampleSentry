using FluentValidation;
using SampleSentry.API.Features.Category.Command;

namespace SampleSentry.API.Features.Category.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        }
    }
}
