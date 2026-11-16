namespace AppointmentService.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid Patientid { get; set; }

        public Guid Doctorid { get; set; }

        public DateOnly Appointmentdate { get; set; }

        public TimeOnly Appointmenttime { get; set; }
    }
}
