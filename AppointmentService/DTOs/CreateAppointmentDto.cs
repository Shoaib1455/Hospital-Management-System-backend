namespace AppointmentService.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }
    }
}
