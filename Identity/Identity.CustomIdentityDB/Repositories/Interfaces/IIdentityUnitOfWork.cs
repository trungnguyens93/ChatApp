using Identity.CustomIdentityDb.Repository;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Repository
{
    public interface IIdentityUnitOfWork : IUnitOfWork<CustomIdentityDbContext>
    {
        INotificationRepository NotificationRepository { get; }
        IGroupRepository GroupRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
    }
}