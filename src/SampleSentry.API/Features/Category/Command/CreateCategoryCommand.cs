using MediatR;
using SampleSentry.API.Common.Base;

namespace SampleSentry.API.Features.Category.Command
{
    public class CreateCategoryCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
