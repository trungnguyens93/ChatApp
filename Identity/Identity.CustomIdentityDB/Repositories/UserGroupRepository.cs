using Identity.CustomIdentityDb.Repository;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Repository
{
    public class UserGroupRepository : GenericRepository<CustomIdentityDbContext, UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(IUnitOfWork<CustomIdentityDbContext> uow) : base(uow)
        {
        }
    }
}