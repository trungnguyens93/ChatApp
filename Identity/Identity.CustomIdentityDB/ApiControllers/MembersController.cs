using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Identity.CustomIdentityDB.Controllers
{
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IConfiguration _configuration;

        public MembersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetNotificationsByMemberId()
        {
            return null;
        }
    }
}