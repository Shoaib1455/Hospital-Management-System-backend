namespace AppointmentService.Interfaces
{
    public interface IPatientServiceClient
    {
        Task<bool> PatientExistsAsync(Guid Patientid);
    }
}
