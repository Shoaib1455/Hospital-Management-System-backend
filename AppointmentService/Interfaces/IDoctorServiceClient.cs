using AppointmentService.DTOs;

namespace AppointmentService.Interfaces
{
    public interface IDoctorServiceClient
    {
        Task<bool> DoctorExistsAsync(Guid Doctorid);

        Task<DoctorResponseDto?> GetDoctorAsync(Guid Doctorid);
    }
}
