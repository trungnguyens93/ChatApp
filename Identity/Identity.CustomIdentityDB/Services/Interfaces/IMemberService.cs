using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Service
{
    public interface IMemberService
    {
        Task<IList<Notification>> GetNotificationsByMemberIdAsync(string memberId);
    }
}