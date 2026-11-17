using System.ComponentModel.DataAnnotations;

namespace PatientService.Models;

public class MedicalConsultation
{
    [Key] public Guid Id { get; set; }
    public Guid AppointmentId { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    [MaxLength(2000)] public string? Diagnosis { get; set; }
    [MaxLength(4000)] public string? Notes { get; set; }
    [MaxLength(4000)] public string? PrescribedMedicines { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<RecommendedTest> RecommendedTests { get; set; } = [];
}

public class RecommendedTest
{
    [Key] public Guid Id { get; set; }
    public Guid ConsultationId { get; set; }
    [MaxLength(200)] public string Name { get; set; } = string.Empty;
    [MaxLength(2000)] public string? Instructions { get; set; }
    [MaxLength(100)] public string Status { get; set; } = "Recommended";
    [MaxLength(4000)] public string? Result { get; set; }
    [MaxLength(1000)] public string? ReportUrl { get; set; }
    public DateTime? ResultUploadedAt { get; set; }
}
