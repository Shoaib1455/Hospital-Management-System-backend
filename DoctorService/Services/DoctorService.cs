using DoctorService.DTOs;
using DoctorService.Interfaces;
using DoctorService.Models;
using System.Numerics;

namespace DoctorService.Services
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Specialization = dto.Specialization,
                ConsultationFee = dto.ConsultationFee,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _repository.AddAsync(doctor);

            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"{doctor.FirstName} {doctor.LastName}",
                Specialization = doctor.Specialization,
                ConsultationFee = doctor.ConsultationFee,
                StartTime = doctor.StartTime,
                EndTime = doctor.EndTime
            };
        }

        public async Task<DoctorDto?> GetByIdAsync(Guid id)
        {
            var doctor = await _repository.GetByIdAsync(id);

            if (doctor == null)
                return null;

            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"{doctor.FirstName} {doctor.LastName}",
                Specialization = doctor.Specialization,
                ConsultationFee = doctor.ConsultationFee,
                StartTime = doctor.StartTime,
                EndTime = doctor.EndTime
            };
        }

        public async Task<List<DoctorDto>> GetAllAsync()
        {
            var doctors = await _repository.GetAllAsync();

            return doctors.Select(x => new DoctorDto
            {
                Id = x.Id,
                FullName = $"{x.FirstName} {x.LastName}",
                Specialization = x.Specialization,
                ConsultationFee = x.ConsultationFee,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateDoctorDto dto)
        {
            var doctor = await _repository.GetByIdAsync(id);

            if (doctor == null)
                throw new Exception("Doctor not found.");

            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.Email = dto.Email;
            doctor.Phone = dto.Phone;
            doctor.Specialization = dto.Specialization;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.StartTime = dto.StartTime;
            doctor.EndTime = dto.EndTime;

            await _repository.UpdateAsync(doctor);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
        
    }
}
