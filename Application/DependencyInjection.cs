using Application.Common.Interface;
using Application.Common.Mapping;
using Application.Master;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppSettingService, AppSettingService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddProfile<AutoMapperProfile>();
        });
        return services;
    }
}
