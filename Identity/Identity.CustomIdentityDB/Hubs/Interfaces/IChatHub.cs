using System.Threading.Tasks;

namespace Identity.CustomIdentityDB.Hub
{
    public interface IChatClient
    {
        Task ReceiveMessage(string memberId, string message);
    }
}