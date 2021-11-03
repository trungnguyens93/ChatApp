using System.Collections.Generic;
using Identity.CustomIdentityDB.Models;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetGroupsByMemberIdQueryRequest : IRequest<IList<Group>>
    {
        public string MemberId { get; set; }
    }
}