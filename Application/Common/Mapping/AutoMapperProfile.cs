using Domain.Master;
using Application.Master.Dto;
using AutoMapper;

namespace Application.Common.Mapping;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppSettingVm, AppSetting>().ReverseMap();
        CreateMap<ReferenceField, ReferenceFieldVm>().ReverseMap();
        CreateMap<CategoryVm, Category>().ReverseMap();
        CreateMap<SubCategoryVm, SubCategory>().ReverseMap();

    }
}
