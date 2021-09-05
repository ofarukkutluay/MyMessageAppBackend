using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Hubs.Chat;
using Microsoft.AspNetCore.SignalR;

namespace Business.Concretes
{
    public class ChatHubManager : IChatHubService
    {
        readonly IHubContext<ChatHub, IChatHubClient> _hubContext;

        public ChatHubManager( IHubContext<ChatHub, IChatHubClient> hubContext)
        {
            
            _hubContext = hubContext;
        }

        public async Task SendMessageToAllClients(string senderEmail, string receiverEmail, string message)
        {
            await _hubContext.Clients.All.ReceiveMessage(senderEmail, message);
            
        }
    }
}
