namespace AppointmentService
{
    public enum AppointmentStatus
    {
        PendingPayment,
        Confirmed,
        Completed,
        Cancelled
    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded 
    }
}
