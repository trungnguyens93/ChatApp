using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Repository;

namespace Identity.CustomIdentityDb.Repository
{
    public class IdentityUnitOfWork : UnitOfWork<CustomIdentityDbContext>, IIdentityUnitOfWork
    {
        private INotificationRepository _notificationRepository;
        private IGroupRepository _groupRepository;
        private IUserGroupRepository _userGroupRepository;

        public IdentityUnitOfWork(CustomIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public INotificationRepository NotificationRepository
        {
            get
            {
                return _notificationRepository ?? new NotificationRepository(this);
            }
        }

        public IGroupRepository GroupRepository
        {
            get
            {
                return _groupRepository ?? new GroupRepository(this);
            }
        }

        public IUserGroupRepository UserGroupRepository
        {
            get
            {
                return _userGroupRepository ?? new UserGroupRepository(this);
            }
        }
    }
}