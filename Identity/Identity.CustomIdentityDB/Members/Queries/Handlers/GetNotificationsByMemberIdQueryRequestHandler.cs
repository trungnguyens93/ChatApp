using System.Threading;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetNotificationsByMemberIdQueryRequestHandler : IRequestHandler<GetNotificationsByMemberIdQueryRequest, Notification>
    {
        public async Task<Notification> Handle(GetNotificationsByMemberIdQueryRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}