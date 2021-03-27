using Capstone.Core.Hubs.Clients;
using Capstone.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Hubs
{
    public class MessageHub : Hub<IChatClient>
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.ReceiveMessage(message);
        }

        public async Task SendMessageToReturn(MessageReturn messageReturn)
        {
            await Clients.All.ReceiveMessageToReturn(messageReturn);
        }

        public async Task SendMessageToBorrow(MessageBorrow messageBorrow)
        {
            await Clients.All.ReceiveMessageToBorrow(messageBorrow);
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
