using AutoMapper;
using MediatR;
using SampleSentry.API.Common.Base;
using SampleSentry.API.Features.Category.Command;
using SampleSentry.API.Features.Category.Validators;
using SampleSentry.API.Repositories.Category;

namespace SampleSentry.API.Features.Category.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly CreateCategoryValidator _validator;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, CreateCategoryValidator validator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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

                var entity = _mapper.Map<Entities.Category>(request);

                await _categoryRepository.CreateCategoryAsync(entity);

                return new BaseResponse
                {
                    Data = entity,
                    Message = "Category created successfully",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw;
            }
        }
    }
}
