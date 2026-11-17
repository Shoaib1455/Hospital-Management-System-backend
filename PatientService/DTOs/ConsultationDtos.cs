namespace PatientService.DTOs;

public class CreateConsultationDto
{
    public Guid AppointmentId { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public string? Diagnosis { get; set; }
    public string? Notes { get; set; }
    public string? PrescribedMedicines { get; set; }
    public List<RecommendedTestDto> RecommendedTests { get; set; } = [];
}

public class RecommendedTestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
}

public class UploadTestResultDto
{
    public string Result { get; set; } = string.Empty;
    public string? ReportUrl { get; set; }
}
