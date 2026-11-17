using DoctorService.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace DoctorService.Controllers
{
    [ApiController]
    [Route("api/appointment-events")]
    public class AppointmentEventsController : ControllerBase
    {
        private readonly ReceivedAppointmentStore _store;

        public AppointmentEventsController(ReceivedAppointmentStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetReceived() => Ok(_store.GetAll());
    }
}
