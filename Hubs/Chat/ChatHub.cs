using System;
using System.Threading.Tasks;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.SignalR;

namespace Hubs.Chat
{
    public class ChatHub : Hub<IChatHubClient>
    {
        private IMessageService;
        private IUserRepository _userServiceRepository;
        private IClientUserRepository _clientUserRepository;
        public ChatHub(IMessageRepository messageRepository, IUserRepository userServiceRepository, IClientUserRepository clientUserRepository)
        {
            _messageRepository = messageRepository;
            _userServiceRepository = userServiceRepository;
            _clientUserRepository = clientUserRepository;
        }

        public async Task SendUserMessage(string senderEmail,string receiverEmail, string message)
        {
            Person senderPerson = _userServiceRepository.GetPersonByEmail(senderEmail);
            Person receiverPerson = _userServiceRepository.GetPersonByEmail(receiverEmail);
            _messageRepository.Add();

            await Clients.Client()
        }


        public async Task SendMessageToAllClients(string email, string message)
        {
            await Clients.All.ReceiveMessage( email, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.UserJoined(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.UserLeaved( Context.ConnectionId);
        }
    }
}
