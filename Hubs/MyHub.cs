﻿using Microsoft.AspNetCore.SignalR;

namespace maksopusSignalRSecond.Hubs
{
    public class MyHub:Hub
    {
        static List<string> clients = new List<string>();
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userJoined", Context.ConnectionId);
            
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userLeaved", Context.ConnectionId);
        }
    }
}
