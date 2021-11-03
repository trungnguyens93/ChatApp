using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Service;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetGroupsByMemberIdQueryRequestHandler : IRequestHandler<GetGroupsByMemberIdQueryRequest, IList<Group>>
    {
        private readonly IMemberService _memberService;

        public GetGroupsByMemberIdQueryRequestHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IList<Group>> Handle(GetGroupsByMemberIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _memberService.GetGroupsByMemberIdAsync(request.MemberId);
        }
    }
}