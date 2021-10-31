using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetNotificationsByMemberIdQueryRequest : IRequest<Notification>
    {
        public string MemberId { get; set; }
    }
}