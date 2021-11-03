using System.Threading;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Service;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetGroupInfoByGroupIdQueryRequestHandler : IRequestHandler<GetGroupInfoByGroupIdQueryRequest, Group>
    {
        private readonly IMemberService _memberService;

        public GetGroupInfoByGroupIdQueryRequestHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<Group> Handle(GetGroupInfoByGroupIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _memberService.GetGroupInfoByGroupIdAsync(request.GroupId);
        }
    }
}