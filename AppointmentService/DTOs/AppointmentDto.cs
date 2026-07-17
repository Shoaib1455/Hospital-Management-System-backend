namespace AppointmentService.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
