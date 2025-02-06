using AutoMapper;
using MediatR;
using SampleSentry.API.Common.Base;
using SampleSentry.API.Features.Category.Command;
using SampleSentry.API.Features.Category.Validators;
using SampleSentry.API.Repositories.Category;

namespace SampleSentry.API.Features.Category.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly UpdateCategoryValidator _validator;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, UpdateCategoryValidator validator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _categoryRepository.GetCategoryById(request.CategoryID);

                if (values == null)
                {
                    return new BaseResponse
                    {
                        IsSuccess = false,
                        Message = "Category not found"
                    };
                }

                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

                    return new BaseResponse
                    {
                        IsSuccess = false,
                        Message = "Validation failed",
                        Errors = errors
                    };
                }

                var category = _mapper.Map<Entities.Category>(request);

                await _categoryRepository.UpdateCategoryAsync(category);

                return new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Category updated successfully"
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
