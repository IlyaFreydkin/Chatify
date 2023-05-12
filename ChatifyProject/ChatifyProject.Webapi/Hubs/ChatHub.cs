using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatifyProject.Webapi.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly List<string> _users = new();
        /// <summary>
        /// Sends the current username from the token to the client.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            if (string.IsNullOrEmpty(Context.UserIdentifier)) { return; }

            lock (_users)
            {
                _users.Add(Context.UserIdentifier);
            }
            var group = Context.User?.Claims.FirstOrDefault(c => c.Type == "Group")?.Value;
            await Clients.All.SendAsync("ReceiveMessage",
                $"{Context.UserIdentifier} in Group {group} joined.");
            await Clients.All.SendAsync("ReceiveUser",
                $"{Context.UserIdentifier}");
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
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (string.IsNullOrEmpty(Context.UserIdentifier)) { return Task.CompletedTask; }

            lock (_users)
            {
                _users.Remove(Context.UserIdentifier);
            }
            return Task.CompletedTask;
        }
        public async Task SendConnectedUsers()
        {
            var users = _users.ToList();
            await Clients.Caller.SendAsync("ReceiveConnectedUsers", users);
        }
    }
}
