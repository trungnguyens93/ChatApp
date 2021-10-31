using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Repository;

namespace Identity.CustomIdentityDB.Service
{
    public class MemberService : BaseService, IMemberService
    {
        public MemberService(IIdentityUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IList<Notification>> GetNotificationsByMemberIdAsync(string memberId)
        {
            var result = await _uow.NotificationRepository.GetAllAsync();
            return result;
        }
    }
}