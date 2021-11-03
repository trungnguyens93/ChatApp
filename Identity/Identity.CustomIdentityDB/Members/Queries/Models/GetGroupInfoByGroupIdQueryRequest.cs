using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetGroupInfoByGroupIdQueryRequest : IRequest<Group>
    {
        public string GroupId { get; set; }
    }
}