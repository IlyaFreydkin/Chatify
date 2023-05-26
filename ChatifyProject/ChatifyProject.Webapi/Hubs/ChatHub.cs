using Bogus.DataSets;
using ChatifyProject.Application.Model;
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
            //var joinedMessage = $"{Context.UserIdentifier} in Group {group} joined.";

            //await Clients.All.SendAsync("ReceiveMessage", new { text = joinedMessage, username = Context.UserIdentifier });
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
            await Clients.All.SendAsync(
                "ReceiveMessage", 
                new { Message = message, Username = Context.UserIdentifier });
        }

        /// <summary>
        /// Overrides the base method to handle disconnection of clients.
        /// </summary>
        /// <param name="exception">An exception that occurred during disconnection.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // If the UserIdentifier is null or empty, there's nothing to do.
            if (string.IsNullOrEmpty(Context.UserIdentifier)) { return Task.CompletedTask; }
            // Lock the users collection to ensure thread safety.
            lock (_users)
            {
                // Remove the disconnected user from the users collection.
                _users.Remove(Context.UserIdentifier);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends the list of connected users to the calling client.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendConnectedUsers()
        {
            // Convert the _users collection to a list
            var users = _users.ToList();
            // Send the list of connected users to the calling client
            Console.WriteLine("Connected Users:");
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
            await Clients.Caller.SendAsync("ReceiveConnectedUsers", users);
        }

        //////////////////////////////////////////Waiting ROOM/////////////////////////////////////////////////////
        public async Task EnterWaitingroom()
        {
            if (Context.UserIdentifier is null) { return; }
            _users.Add(Context.UserIdentifier);
            Console.WriteLine("Connected Users:");
            foreach (var user in _users)
            {
                Console.WriteLine(user);
            }
            await Clients.All.SendAsync("SetWaitingroomState", _users);
        }

        //public async Task ChallengeUser(string challenged)
        //{
        //    string[] users = { Context.User?.Identity?.Name, challenged };
        //    string usersString = Context.User?.Identity?.Name + challenged;
        //    Groups.AddToGroupAsync(Context.ConnectionId, usersString);
        //    await Clients.All.SendAsync("GetChallenges", users);
        //    Console.WriteLine(users.ToString());
        //}

        public async Task LeaveWaitingroom()
        {
            if (Context.User?.Identity?.Name is null) { return; }
            // ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { Context.User?.Identity?.Name }));
            var users = _users.ToList();
            await Clients.All.SendAsync("SetWaitingroomState", users);
        }

        //public async Task StartGame(string challenger)
        //{
        //    string challenged = Context.User.Identity.Name;

        //    string users = challenger + challenged;
        //    Groups.AddToGroupAsync(Context.ConnectionId, users);

        //    Game game = new Game(player1: challenger, player2: challenged);
        //    _db.Games.Add(game);
        //    try { _db.SaveChanges(); }
        //    catch { return; }

        //    await Clients.Group(users).SendAsync("GameStarted", game.Guid.ToString());

        //    ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { challenged, challenger }));
        //    await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);

        //}
    }
}

