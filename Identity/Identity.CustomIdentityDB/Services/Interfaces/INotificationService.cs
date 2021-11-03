using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;

namespace Identity.CustomIdentityDB.Service
{
    public interface INotificationService
    {
        Task AddNotification(Notification notification);
    }
}