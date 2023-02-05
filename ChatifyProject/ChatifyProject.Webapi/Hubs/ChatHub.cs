﻿using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace ChatifyProject.Webapi.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// Sends the current username from the token to the client.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var group = Context.User?.Claims.FirstOrDefault(c => c.Type == "Group")?.Value;
            await Clients.All.SendAsync("ReceiveMessage",
                $"{Context.UserIdentifier} in Group {group} joined.");
        }
        /// <summary>
        /// Invoked by
        ///     connection.invoke("SendMessage", message);
        /// in Javascript SignalR client.
        /// </summary>
        public async Task SendMessage(string message)
        {
            // Can be received with
            //     connection.on("ReceiveMessage", callback);
            // in Javascript SignalR client.
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
