using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSentry.API.Common.Base;
using SampleSentry.API.Features.ApplicationUser.Commands;
using SampleSentry.API.Features.ApplicationUser.Validators;

namespace SampleSentry.API.Features.ApplicationUser.Handlers
{
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, BaseResponse>
    {
        private readonly UserManager<Entities.ApplicationUser> _userManager;
        private readonly CreateApplicationUserValidator _validator;

        public CreateApplicationUserCommandHandler(UserManager<Entities.ApplicationUser> userManager, CreateApplicationUserValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<BaseResponse> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

                    return new BaseResponse
                    {
                        Message = "Validation failed",
                        Errors = errors
                    };
                }

                var user = new Entities.ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname
                };

                await _userManager.CreateAsync(user, request.Password);

                return new BaseResponse
                {
                    Message = "User created successfully",
                };
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw new Exception("An error occurred while creating the user", ex);
            }
        }
    }
}
