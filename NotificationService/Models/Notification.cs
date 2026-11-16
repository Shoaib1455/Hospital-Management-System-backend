namespace NotificationService.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid Patientid { get; set; }
        public Guid Doctorid { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
