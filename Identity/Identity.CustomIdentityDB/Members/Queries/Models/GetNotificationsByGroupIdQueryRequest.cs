using System.Collections.Generic;
using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetNotificationsByGroupIdQueryRequest : IRequest<IList<Notification>>
    {
        public string GroupId { get; set; }
    }
}