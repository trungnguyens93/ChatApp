using System;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Hub;
using Identity.CustomIdentityDB.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Identity.CustomIdentityDB.Controllers
{
    [ApiController]
    [Route("ntf/messages")]
    public class MessagesController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly INotificationService _notificationService;

        public MessagesController(IHubContext<ChatHub> hubContext, INotificationService notificationService)
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        [HttpPost("sendmessage")]
        public async Task<IActionResult> SendMessageAsync(string groupId, string memberId, string message)
        {
            await _notificationService.AddNotification(new Models.Notification
            {
                Id = Guid.NewGuid().ToString(),
                Content = message,
                Sender = memberId,
                CreatedAt = DateTime.Now
            });

            await _hubContext.Clients.Group(groupId).SendAsync("sendMessage", memberId, message);
            return Ok();
        }
    }
}