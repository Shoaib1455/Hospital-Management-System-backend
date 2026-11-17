using AppointmentService.Models;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);

        Task<Appointment?> GetByIdAsync(Guid id);

        Task<List<Appointment>> GetAllAsync();
        Task<List<Appointment>> GetByDoctorAsync(Guid doctorId);
        Task<List<Appointment>> GetByPatientAsync(Guid patientId);

        Task<bool> IsSlotBooked(Guid Doctorid,
                                DateOnly date,
                                TimeOnly time);

        Task UpdateAsync(Appointment appointment);

        Task DeleteAsync(Guid id);
    }
}
