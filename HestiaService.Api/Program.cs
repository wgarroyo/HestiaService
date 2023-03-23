using HestiaService.Api;
using HestiaService.Api.WebSockets;
using HestiaService.Application;
using HestiaService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddPresentation()
       .AddApplication()
       .AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

IServiceScopeFactory serviceFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
IServiceProvider serviceProvider = serviceFactory.CreateScope().ServiceProvider;
NotificationsMessageHandler? service = serviceProvider.GetService<NotificationsMessageHandler>();

if (service is null)
    return;

app.MapWebSocketManager("/ws", service);
//app.UseAuthorization();
app.MapControllers();
app.Run();
