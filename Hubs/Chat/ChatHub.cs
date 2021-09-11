using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.SignalR;

namespace Hubs.Chat
{
    
    public class ChatHub : Hub<IChatHubClient>
    {
        
        private readonly IClientUserRepository _clientUserRepository;

        
        public ChatHub(IClientUserRepository clientUserRepository)
        {
            _clientUserRepository = clientUserRepository;
        }


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


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ClientUser clientUser = _clientUserRepository.SearchFor(cs => cs.ClientId == Context.ConnectionId).FirstOrDefault();
            _clientUserRepository.Delete(clientUser);
            await Clients.All.UserLeaved($"{clientUser.UserEmail} çıkış yaptı");
        }
    }
}
