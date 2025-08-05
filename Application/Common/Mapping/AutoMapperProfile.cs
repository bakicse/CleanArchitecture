using Application.Master.Dto;
using Application.Master.ViewModel;
using Domain.Master;
using AutoMapper;

namespace Application.Common.Mapping;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppSetting, AppSettingVm>().ReverseMap();
        CreateMap<ReferenceField, ReferenceFieldVm>().ReverseMap();
        CreateMap<Category, CategoryVm>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<SubCategory, SubCategoryVm>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty));

        CreateMap<SubCategoryDto, SubCategory>().ReverseMap();

    }
}
