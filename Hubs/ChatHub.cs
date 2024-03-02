using Microsoft.AspNetCore.SignalR;
using textME.Models;

namespace textME.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(UserConnection connection)
        {
            await Clients.All.SendAsync("ReciveMessages", "admin", $"{connection.UserName} joined the chat");
        }

        public async Task JoinSpecificRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.Group);
            await Clients.Group(connection.Group).SendAsync("ReciveMessages", "admin", $"{connection.UserName} joined the {connection.Group} chat");
        }

        public async Task SendMessage(Message message)
        {
            await Clients.Group(message.Group).SendAsync("ReciveMessages", "admin", message.MessageText);
        }

    }
}
