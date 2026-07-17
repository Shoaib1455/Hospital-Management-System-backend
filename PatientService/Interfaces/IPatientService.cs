using PatientService.DTOs;

namespace PatientService.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreateAsync(CreatePatientDto dto);

        Task<PatientDto?> GetByIdAsync(Guid id);
    }
}
