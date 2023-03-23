using HestiaService.Api.Common.Mapping;
using HestiaService.Api.WebSockets;

namespace HestiaService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddWebSocketManager();

        return services;
    }
}
