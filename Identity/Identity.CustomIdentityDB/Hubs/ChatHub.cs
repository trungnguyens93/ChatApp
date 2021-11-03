using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Identity.CustomIdentityDB.Hub
{
    public class ChatHub : Hub<IChatClient>
    {
        public Task SendMessage(string memberId, string message)
        {
            return Clients.All.ReceiveMessage(memberId, message);
        }

        public Task SendMessageToCaller(string memberId, string message)
        {
            return Clients.Caller.ReceiveMessage(memberId, message);
        }

        public Task SendMessageToGroup(string memberId, string message)
        {
            string groupName = string.Empty;
            return Clients.Groups(groupName).ReceiveMessage(memberId, message);
        }

        public Task RegisterToGroup(string groupId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public Task RemoveFromGroup(string groupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }
    }
}