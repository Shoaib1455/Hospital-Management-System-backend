using DoctorService.Models;
using System.Numerics;

namespace DoctorService.Interfaces
{
    public interface IDoctorRepository
    {
        public Task AddAsync(Doctor doctor);

        public Task<Doctor?> GetByIdAsync(Guid id);

        public Task<List<Doctor>> GetAllAsync();

        public  Task UpdateAsync(Doctor doctor);

        public Task DeleteAsync(Guid id);
    }
}
