using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Service
{
    public interface IMemberService
    {
        Task<Group> GetGroupInfoByGroupIdAsync(string groupId);
        Task<IList<Group>> GetGroupsByMemberIdAsync(string memberId);
        Task<IList<Notification>> GetNotificationsByGroupIdAsync(string groupId);
        Task AddNewGroupForMemberAsync(string groupName);
    }
}