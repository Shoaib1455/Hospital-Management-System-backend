using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using PatientService.Data;
using PatientService.DTOs;
using PatientService.Models;

namespace PatientService.Controllers;

[ApiController]
[Route("api")]
public class MedicalRecordsController(PatientDbContext db, IHttpClientFactory clients) : ControllerBase
{
    [HttpPost("appointments/{appointmentId:guid}/consultation")]
    public async Task<IActionResult> SaveConsultation(Guid appointmentId, CreateConsultationDto dto)
    {
        if (appointmentId != dto.AppointmentId || dto.PatientId == Guid.Empty || dto.DoctorId == Guid.Empty)
            return BadRequest("Appointment, patient, and doctor IDs are required and must match.");
        if (!await db.Patients.AnyAsync(p => p.Id == dto.PatientId)) return NotFound("Patient not found.");
        var existing = await db.MedicalConsultations.Include(x => x.RecommendedTests).SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
        if (existing != null) return Conflict("A consultation has already been recorded for this appointment.");
        var record = new MedicalConsultation
        {
            Id = Guid.NewGuid(), AppointmentId = appointmentId, PatientId = dto.PatientId, DoctorId = dto.DoctorId,
            Diagnosis = dto.Diagnosis, Notes = dto.Notes, PrescribedMedicines = dto.PrescribedMedicines,
            RecommendedTests = dto.RecommendedTests.Where(t => !string.IsNullOrWhiteSpace(t.Name)).Select(t => new RecommendedTest
            { Id = Guid.NewGuid(), Name = t.Name.Trim(), Instructions = t.Instructions }).ToList()
        };
        db.MedicalConsultations.Add(record);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPatientHistory), new { patientId = record.PatientId }, record);
    }

    [HttpPut("lab/tests/{testId:guid}/result")]
    public async Task<IActionResult> UploadResult(Guid testId, UploadTestResultDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Result)) return BadRequest("A test result is required.");
        var test = await db.RecommendedTests.FindAsync(testId);
        if (test == null) return NotFound();
        test.Result = dto.Result.Trim();
        test.ReportUrl = dto.ReportUrl;
        test.Status = "Completed";
        test.ResultUploadedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return Ok(test);
    }

    [HttpGet("patients/{patientId:guid}/history")]
    public async Task<IActionResult> GetPatientHistory(Guid patientId)
    {
        if (!await db.Patients.AnyAsync(p => p.Id == patientId)) return NotFound("Patient not found.");
        var records = await db.MedicalConsultations.Include(x => x.RecommendedTests)
            .Where(x => x.PatientId == patientId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        var response = await clients.CreateClient("appointment-service")
            .GetAsync($"api/appointments/patient/{patientId}");
        if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode, "Could not load appointment history.");
        var appointments = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
        return Ok(new { appointments, consultations = records });
    }
}
