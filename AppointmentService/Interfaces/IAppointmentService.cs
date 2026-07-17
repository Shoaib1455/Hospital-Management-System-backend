using AppointmentService.DTOs;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentService
    {
        public Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);

        public Task<List<AppointmentDto>> GetAllAsync();

        public Task<AppointmentDto?> GetByIdAsync(Guid id);

        public Task CancelAsync(Guid id);
    }
}
