using MediatR;
using SampleSentry.API.Common.Base;

namespace SampleSentry.API.Features.ApplicationUser.Commands
{
    public class CreateApplicationUserCommand : IRequest<BaseResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
