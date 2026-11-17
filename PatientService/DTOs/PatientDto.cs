namespace PatientService.DTOs
{
    public class PatientDto
    {
        public string Patientid { get; set; }
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
