namespace AppointmentService.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public Guid Patientid { get; set; }

        public Guid Doctorid { get; set; }

        public DateOnly Appointmentdate { get; set; }
        public TimeOnly Appointmenttime { get; set; }

        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
