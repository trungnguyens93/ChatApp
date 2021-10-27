using Identity.CustomIdentityDb.Repository;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Repository
{
    public class NotificationRepository : GenericRepository<CustomIdentityDbContext, Notification>, INotificationRepository
    {
        public NotificationRepository(IUnitOfWork<CustomIdentityDbContext> uow) : base(uow)
        {
        }
    }
}