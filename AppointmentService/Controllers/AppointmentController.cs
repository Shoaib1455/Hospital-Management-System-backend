using AppointmentService.DTOs;
using AppointmentService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var appointment = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = appointment.Id },
                appointment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetForDoctor(Guid doctorId) => Ok(await _service.GetByDoctorAsync(doctorId));

        [HttpGet("patient/{patientId:guid}")]
        public async Task<IActionResult> GetForPatient(Guid patientId) => Ok(await _service.GetByPatientAsync(patientId));

        [HttpGet("slots")]
        public async Task<IActionResult> GetAvailableSlots([FromQuery] Guid doctorId, [FromQuery] DateOnly date)
        {
            if (doctorId == Guid.Empty) return BadRequest("doctorId is required.");
            try { return Ok(await _service.GetAvailableSlotsAsync(doctorId, date)); }
            catch (Exception ex) when (ex.Message == "Doctor not found.") { return NotFound(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _service.GetByIdAsync(id);

            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _service.CancelAsync(id);

            return NoContent();
        }
    }
}
