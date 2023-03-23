using System.Reflection;

namespace HestiaService.Api.WebSockets;

public static class WebSocketManagerExtensions
{
    public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
    {
        services.AddTransient<ConnectionManager>();

        foreach (var type in Assembly.GetExecutingAssembly().ExportedTypes)
        {
            if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }

    public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                          PathString path,
                                                          WebSocketHandler handler)
    {
        return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
    }
}
