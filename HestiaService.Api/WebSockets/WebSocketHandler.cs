﻿using System.Net.WebSockets;
using System.Text;

namespace HestiaService.Api.WebSockets;

public abstract class WebSocketHandler
{
    protected ConnectionManager WebSocketConnectionManager { get; set; }

    public WebSocketHandler(ConnectionManager webSocketConnectionManager)
    {
        WebSocketConnectionManager = webSocketConnectionManager;
    }

    public virtual async Task OnConnectedAsync(WebSocket socket)
    {        
        WebSocketConnectionManager.AddSocket(socket);
        await Task.CompletedTask;
    }

    public virtual async Task OnDisconnectedAsync(WebSocket socket)
    {
        await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
    }

    public async Task SendMessageAsync(WebSocket socket, string message)
    {
        if (socket.State != WebSocketState.Open)
            return;

        await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                              offset: 0,
                                                              count: message.Length),
                               messageType: WebSocketMessageType.Text,
                               endOfMessage: true,
                               cancellationToken: CancellationToken.None);
    }

    public async Task SendMessageAsync(string socketId, string message)
    {
        await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
    }

    public async Task SendMessageToAllAsync(string message)
    {
        foreach (var pair in WebSocketConnectionManager.GetAll())
        {
            if (pair.Value.State == WebSocketState.Open)
                await SendMessageAsync(pair.Value, message);
        }
    }

    //TODO - decide if exposing the message string is better than exposing the result and buffer
    public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
}
