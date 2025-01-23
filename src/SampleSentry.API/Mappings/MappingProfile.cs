using AutoMapper;
using SampleSentry.API.Features.Category.Command;

namespace SampleSentry.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryCommand, Entities.Category>().ReverseMap();
        }
    }
}
