using MediatR;
using SampleSentry.API.Common.Base;

namespace SampleSentry.API.Features.Category.Command
{
    public class UpdateCategoryCommand : IRequest<BaseResponse>
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
