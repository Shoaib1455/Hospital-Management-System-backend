using Microsoft.AspNetCore.Mvc;
using PatientService.DTOs;
using PatientService.Interfaces;

namespace PatientService.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            var patient = await _patientService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { patientId = patient.PatientId },
                patient);
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetById(Guid patientId)
        {
            var patient = await _patientService.GetByIdAsync(patientId);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }
    }
}
