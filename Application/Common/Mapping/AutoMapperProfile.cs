using Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Master.Dto;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common.Mapping;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppSettingVm, AppSetting>().ReverseMap();
        CreateMap<ReferenceField, ReferenceFieldVm>().ReverseMap();

    }
}
