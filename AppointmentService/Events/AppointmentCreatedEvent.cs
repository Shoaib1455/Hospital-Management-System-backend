namespace AppointmentService.Events
{
    public class AppointmentCreatedEvent
    {
        public Guid AppointmentId { get; set; }

        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public decimal Amount { get; set; }

        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public decimal ConsultationFee { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
