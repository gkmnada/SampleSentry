using FluentValidation;
using SampleSentry.API.Features.ApplicationUser.Commands;

namespace SampleSentry.API.Features.ApplicationUser.Validators
{
    public class CreateApplicationUserValidator : AbstractValidator<CreateApplicationUserCommand>
    {
        public CreateApplicationUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required")
                .MaximumLength(50).WithMessage("Surname must not exceed 50 characters");
        }
    }
}
