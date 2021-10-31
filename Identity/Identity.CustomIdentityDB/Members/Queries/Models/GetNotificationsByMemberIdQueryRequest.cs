using System.Collections.Generic;
using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetNotificationsByMemberIdQueryRequest : IRequest<IList<Notification>>
    {
        public string MemberId { get; set; }
    }
}