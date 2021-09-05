using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IChatHubService
    {
        Task SendMessageToAllClients(string senderEmail, string receiverEmail, string message);
    }
}
