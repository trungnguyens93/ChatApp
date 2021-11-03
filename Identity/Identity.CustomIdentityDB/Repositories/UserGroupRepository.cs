using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.CustomIdentityDb.Repository;
using Identity.CustomIdentityDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.CustomIdentityDB.Repository
{
    public class UserGroupRepository : GenericRepository<CustomIdentityDbContext, UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(IUnitOfWork<CustomIdentityDbContext> uow) : base(uow)
        {
        }

        public async Task<IList<Group>> GetGroupsByMemberIdAsync(string memberId)
        {
            var result = await this.FindByAsync(p => p.UserId == memberId);
            return new List<Group>();
        }
    }
}