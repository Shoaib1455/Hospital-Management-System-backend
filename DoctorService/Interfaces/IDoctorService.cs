using DoctorService.DTOs;

namespace DoctorService.Interfaces
{
    public interface IDoctorService
    {
        public Task<DoctorDto> CreateAsync(CreateDoctorDto dto);

        public Task<DoctorDto?> GetByIdAsync(Guid id);

        public Task<List<DoctorDto>> GetAllAsync();

        public Task UpdateAsync(Guid id, UpdateDoctorDto dto);

        public Task DeleteAsync(Guid id);
    }
}
