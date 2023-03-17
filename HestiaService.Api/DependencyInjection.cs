using HestiaService.Api.Common.Mapping;

namespace HestiaService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
