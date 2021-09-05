using System.Threading.Tasks;

namespace Hubs.Chat
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string email,string message);
        Task UserJoined(string connectionId);
        Task UserLeaved(string connectionId);
    }
}
