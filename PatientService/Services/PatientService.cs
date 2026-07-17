using PatientService.DTOs;
using PatientService.Interfaces;
using PatientService.Models;

namespace PatientService.Services
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth
            };

            await _repository.AddAsync(patient);

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
        }

        public async Task<PatientDto?> GetByIdAsync(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);

            if (patient == null)
                return null;

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Phone = patient.Phone
            };
        }

    }
}
