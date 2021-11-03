using System;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Repository;

namespace Identity.CustomIdentityDB.Service
{
    public class NotificationService : BaseService, INotificationService
    {
        private IServiceProvider _serviceProvider;

        public NotificationService(IServiceProvider serviceProvider, IIdentityUnitOfWork uow) : base(uow)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AddNotification(Notification notification)
        {
            await _uow.NotificationRepository.InsertAsync(notification);
        }
    }
}