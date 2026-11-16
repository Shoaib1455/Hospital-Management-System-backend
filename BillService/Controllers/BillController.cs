using BillService.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace BillService.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillController : ControllerBase
    {
        private readonly BillStore _store;

        public BillController(BillStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_store.GetAll());
    }
}
