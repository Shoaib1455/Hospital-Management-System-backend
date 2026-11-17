using Microsoft.AspNetCore.Mvc;
using PatientService.DTOs;
using PatientService.Interfaces;

namespace PatientService.Controllers
{
    [ApiController]
    [Route("api/Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            var patient = await _patientService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { Patientid = patient.Id },
                patient);
        }

        [HttpGet("{Patientid}")]
        public async Task<IActionResult> GetById(Guid Patientid)
        {
            var patient = await _patientService.GetByIdAsync(Patientid);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }
    }
}
