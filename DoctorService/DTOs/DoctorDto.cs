namespace DoctorService.DTOs
{
    public class DoctorDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public decimal ConsultationFee { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
