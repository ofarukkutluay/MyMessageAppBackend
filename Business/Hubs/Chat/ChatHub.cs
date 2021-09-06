using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.SignalR;

namespace Business.Hubs.Chat
{
    public class ChatHub : Hub<IChatHubClient>
    {
        //private readonly IMessageService _messageService;
        //private readonly IUserService _userService;
        private readonly IClientUserRepository _clientUserRepository;

        /*public ChatHub(IMessageService messageService, IUserService userService, IClientUserRepository clientUserRepository)
        {
            _messageService = messageService;
            _userService = userService;
            _clientUserRepository = clientUserRepository;
        }*/

        public ChatHub(IClientUserRepository clientUserRepository)
        {
            _clientUserRepository = clientUserRepository;
        }

        /* public async Task SendUserMessage(string senderEmail, string receiverEmail, string message)
         {
             var senderPerson = _userService.GetPersonByEmail(senderEmail);
             var receiverPerson = _userService.GetPersonByEmail(receiverEmail);
             var result = _messageService.Add(new Message()
             {
                 Text = message,
                 SenderUserId = senderPerson.Data.Id,
                 ReciverUserId = receiverPerson.Data.Id
             });
             switch (result.Success)
             {
                 case false:
                     await Clients.Caller.ReceiveMessage(receiverEmail, "Bu kullanıcıya Mesaj Gönderilemez!");
                     break;
                 case true:
                     {
                         ClientUser clientUser = _clientUserRepository.SearchFor(cu => cu.UserEmail == receiverEmail).FirstOrDefault();
                         if (clientUser == null)
                         {
                             await Clients.Caller.ReceiveMessage(senderEmail, message);
                             break;
                         }
                         await Clients.Caller.ReceiveMessage("You", message);
                         await Clients.Client(clientUser.ClientId).ReceiveMessage(senderEmail, message);
                         break;
                     }
             }
         }*/

        public async Task LoginUser(Person person)
        {
            var clientUser = _clientUserRepository.SearchFor(cu => cu.UserId == person.Id).FirstOrDefault();
            if (clientUser != null)
            {
                clientUser.ClientId = Context.ConnectionId;
                _clientUserRepository.Update(clientUser);
                await Clients.Others.UserJoined($"{person.Email} tekrar giriş yaptı");
            }
            else
            {
                _clientUserRepository.Insert(new ClientUser()
                {
                    ClientId = Context.ConnectionId,
                    UserId = person.Id,
                    UserEmail = person.Email
                });
            }
            await Clients.Others.UserJoined($"{person.Email} giriş yaptı");

        }

        public override async Task OnConnectedAsync()
        {
            ClientUser clientUser = _clientUserRepository.SearchFor(cs => cs.ClientId == Context.ConnectionId).FirstOrDefault();
            if (clientUser != null)
            {
                clientUser.ClientId = Context.ConnectionId;
                _clientUserRepository.Update(clientUser);
                await Clients.Others.UserJoined($"{clientUser.UserEmail} tekrar giriş yaptı");
            }
        }


        /*public async Task SendMessageToAllClients(string email, string message)
        {
            await Clients.All.ReceiveMessage(email, message);
        }*/


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ClientUser clientUser = _clientUserRepository.SearchFor(cs => cs.ClientId == Context.ConnectionId).FirstOrDefault();
            _clientUserRepository.Delete(clientUser);
            await Clients.All.UserLeaved($"{clientUser.UserEmail} çıkış yaptı");
        }
    }
}
