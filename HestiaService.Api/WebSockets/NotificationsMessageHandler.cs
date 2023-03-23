using System.Net.WebSockets;

namespace HestiaService.Api.WebSockets;

public class NotificationsMessageHandler : WebSocketHandler
{
    public NotificationsMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
    {

    }

    public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        throw new NotImplementedException();
    }

}
