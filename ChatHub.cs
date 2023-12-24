using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace RentSpace
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string toId, string fromId, string message)
        {
            // Save the message to the database using Entity Framework
            // ...

            // Send the message to the specified user(s)
            await Clients.User(toId).SendAsync("ReceiveMessage", fromId, message);
            await Clients.User(fromId).SendAsync("ReceiveMessage", fromId, message);
        }
    }
}