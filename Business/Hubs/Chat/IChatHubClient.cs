using System.Threading.Tasks;

namespace Business.Hubs.Chat
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string email,string message);
        Task UserJoined(string serverMessage);
        Task UserLeaved(string serverMessage);
    }
}
