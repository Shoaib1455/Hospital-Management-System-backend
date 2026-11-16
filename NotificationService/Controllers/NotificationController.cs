using Microsoft.AspNetCore.Mvc;
using NotificationService.Messaging;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationStore _store;

        public NotificationController(NotificationStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_store.GetAll());
    }
}
