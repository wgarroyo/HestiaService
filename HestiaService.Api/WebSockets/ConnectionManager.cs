using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace HestiaService.Api.WebSockets;

public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

    public WebSocket GetSocketById(string id)
    {
        return _sockets.FirstOrDefault(p => p.Key == id).Value;
    }

    public ConcurrentDictionary<string, WebSocket> GetAll()
    {
        return _sockets;
    }

    public string GetId(WebSocket socket)
    {
        return _sockets.FirstOrDefault(p => p.Value == socket).Key;
    }
    public void AddSocket(WebSocket socket)
    {
        _sockets.TryAdd(CreateConnectionId(), socket);
    }

    public async Task RemoveSocket(string id)
    {
        _sockets.TryRemove(id, out WebSocket? socket);

        if (socket is null)
            return;

        await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                statusDescription: "Closed by the ConnectionManager",
                                cancellationToken: CancellationToken.None);
    }

    private static string CreateConnectionId()
    {
        return Guid.NewGuid().ToString();
    }
}
