namespace NotificationService.Events
{
    public class AppointmentCreatedEvent
    {
        public Guid AppointmentId { get; set; }
        public Guid Patientid { get; set; }
        public Guid Doctorid { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Appointmentdate { get; set; }
        public TimeOnly Appointmenttime { get; set; }
        public decimal ConsultationFee { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
