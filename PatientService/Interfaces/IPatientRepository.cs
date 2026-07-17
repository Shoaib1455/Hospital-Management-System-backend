using PatientService.Models;

namespace PatientService.Interfaces
{
    public interface IPatientRepository
    {
        public Task<IEnumerable<Patient>> GetAllAsync();

        public Task<Patient?> GetByIdAsync(Guid id);

        public Task AddAsync(Patient patient);

        public Task UpdateAsync(Patient patient);

        public Task DeleteAsync(Guid id);

        public Task<bool> ExistsAsync(Guid id);
    }
}
