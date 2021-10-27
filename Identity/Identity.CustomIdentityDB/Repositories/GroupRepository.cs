using Identity.CustomIdentityDb.Repository;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Repository
{
    public class GroupRepository : GenericRepository<CustomIdentityDbContext, Group>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork<CustomIdentityDbContext> uow) : base(uow)
        {
        }
    }
}