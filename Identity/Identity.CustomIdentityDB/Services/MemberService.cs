using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Group>> GetGroupsByMemberIdAsync(string memberId)
        {
            var userGroups = await _uow.UserGroupRepository.FindByAsync(p => p.UserId == memberId);
            return await _uow.GroupRepository.FindByAsync(p => userGroups.Select(x => x.GroupId).Contains(p.Id));
        }

        public async Task<IList<Notification>> GetNotificationsByGroupIdAsync(string groupId)
        {
            return await _uow.NotificationRepository.FindByAsync(p => p.GroupId == groupId);
        }

        public Task AddNewGroupForMemberAsync(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Group> GetGroupInfoByGroupIdAsync(string groupId)
        {
            return await _uow.GroupRepository.FindSingleAsync(p => p.Id == groupId);
        }
    }
}