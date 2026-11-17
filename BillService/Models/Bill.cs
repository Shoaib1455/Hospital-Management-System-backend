namespace BillService.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid Patientid { get; set; }
        public Guid Doctorid { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
