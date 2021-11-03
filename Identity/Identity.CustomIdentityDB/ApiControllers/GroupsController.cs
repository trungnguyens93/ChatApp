using System.Threading.Tasks;
using Identity.CustomIdentityDB.Member.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Identity.CustomIdentityDB.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public GroupsController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupInfoAsync(string id)
        {
            var request = new GetGroupInfoByGroupIdQueryRequest
            {
                GroupId = id
            };

            var result = await _mediator.Send(request);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetNotificationsByGroupIdAsync(string id)
        {
            var request = new GetNotificationsByGroupIdQueryRequest
            {
                GroupId = id
            };

            var result = await _mediator.Send(request);
            if (request != null)
                return Ok(result);

            return NotFound();
        }
    }
}