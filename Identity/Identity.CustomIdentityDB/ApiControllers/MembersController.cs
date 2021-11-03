using System.Threading.Tasks;
using Identity.CustomIdentityDB.Member.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Identity.CustomIdentityDB.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public MembersController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet("{id}/groups")]
        public async Task<IActionResult> GetGroupsByMemberId(string id)
        {
            var request = new GetGroupsByMemberIdQueryRequest
            {
                MemberId = id
            };

            var result = await _mediator.Send(request);
            if (request != null)
                return Ok(result);

            return NotFound();
        }
    }
}