using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message message);
        Task ReceiveMessageToReturn(MessageReturn messageReturn);
        Task ReceiveMessageToBorrow(MessageBorrow messageBorrow);
    }
}
