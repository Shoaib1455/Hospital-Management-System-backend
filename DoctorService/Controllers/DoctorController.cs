using DoctorService.DTOs;
using DoctorService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorService.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        
            public DoctorController(IDoctorService doctorService)
            {
                _doctorService = doctorService;
            }

            [HttpPost]
            public async Task<IActionResult> Create(CreateDoctorDto dto)
            {
                var doctor = await _doctorService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = doctor.Id },
                    doctor);
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _doctorService.GetAllAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var doctor = await _doctorService.GetByIdAsync(id);

                if (doctor == null)
                    return NotFound();

                return Ok(doctor);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, UpdateDoctorDto dto)
            {
                await _doctorService.UpdateAsync(id, dto);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                await _doctorService.DeleteAsync(id);

                return NoContent();
            }
        }
    }

