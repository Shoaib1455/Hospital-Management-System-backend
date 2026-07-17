using AppointmentService.Models;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);

        Task<Appointment?> GetByIdAsync(Guid id);

        Task<List<Appointment>> GetAllAsync();

        Task<bool> IsSlotBooked(Guid doctorId,
                                DateOnly date,
                                TimeOnly time);

        Task UpdateAsync(Appointment appointment);

        Task DeleteAsync(Guid id);
    }
}
