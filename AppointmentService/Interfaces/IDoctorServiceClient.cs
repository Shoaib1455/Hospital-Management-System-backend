using AppointmentService.DTOs;

namespace AppointmentService.Interfaces
{
    public interface IDoctorServiceClient
    {
        Task<bool> DoctorExistsAsync(Guid doctorId);

        Task<DoctorResponseDto?> GetDoctorAsync(Guid doctorId);
    }
}
