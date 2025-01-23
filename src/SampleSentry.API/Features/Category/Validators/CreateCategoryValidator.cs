using FluentValidation;
using SampleSentry.API.Features.Category.Command;

namespace SampleSentry.API.Features.Category.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least 2 character")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        }
    }
}
