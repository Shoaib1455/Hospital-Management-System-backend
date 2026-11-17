using AppointmentService.DTOs;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentService
    {
        public Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);

        public Task<List<AppointmentDto>> GetAllAsync();
        public Task<List<AppointmentDto>> GetByDoctorAsync(Guid doctorId);
        public Task<List<AppointmentDto>> GetByPatientAsync(Guid patientId);
        public Task<List<TimeOnly>> GetAvailableSlotsAsync(Guid doctorId, DateOnly date);

        public Task<AppointmentDto?> GetByIdAsync(Guid id);

        public Task CancelAsync(Guid id);
    }
}
