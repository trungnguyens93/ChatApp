using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Service;
using MediatR;

namespace Identity.CustomIdentityDB.Member.Query
{
    public class GetNotificationsByMemberIdQueryRequestHandler : IRequestHandler<GetNotificationsByGroupIdQueryRequest, IList<Notification>>
    {
        private readonly IMemberService _memberService;

        public GetNotificationsByMemberIdQueryRequestHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IList<Notification>> Handle(GetNotificationsByGroupIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _memberService.GetNotificationsByGroupIdAsync(request.GroupId);
        }
    }
}