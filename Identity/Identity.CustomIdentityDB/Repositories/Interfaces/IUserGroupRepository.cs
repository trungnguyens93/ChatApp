using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Repository
{
    public interface IUserGroupRepository : IGenericRepository<UserGroup>
    {
        Task<IList<Group>> GetGroupsByMemberIdAsync(string memberId);
    }
}