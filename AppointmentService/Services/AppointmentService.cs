using AppointmentService.DTOs;
using AppointmentService.Events;
using AppointmentService.Interfaces;
using AppointmentService.Models;

namespace AppointmentService.Services
{
    public class AppointmentService:IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPatientServiceClient _patientClient;
        private readonly IDoctorServiceClient _doctorClient;
        private readonly IEventPublisher _publisher;
        public AppointmentService(
        IAppointmentRepository repository,
        IPatientServiceClient patientClient,
        IDoctorServiceClient doctorClient,
        IEventPublisher publisher)
        {
            _repository = repository;
            _patientClient = patientClient;
            _doctorClient = doctorClient;
            _publisher = publisher;
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            // Verify patient exists
            var patientExists =
                await _patientClient.PatientExistsAsync(dto.PatientId);

            if (!patientExists)
                throw new Exception("Patient not found.");

            // Verify doctor exists
            var doctor =
                await _doctorClient.GetDoctorAsync(dto.DoctorId);

            if (doctor == null)
                throw new Exception("Doctor not found.");

            // Verify doctor is available
            var slotBooked =
                await _repository.IsSlotBooked(
                    dto.DoctorId,
                    dto.AppointmentDate,
                    dto.AppointmentTime);

            if (slotBooked)
                throw new Exception("Slot already booked.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Status = AppointmentStatus.PendingPayment
            };

            await _repository.AddAsync(appointment);

            await _publisher.PublishAsync("appointment.created",new AppointmentCreatedEvent
            {
                AppointmentId = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                ConsultationFee = doctor.ConsultationFee
            });

            return new AppointmentDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = appointment.Status
            };
        }
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
        public async Task<AppointmentDto?> GetByIdAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);

            if (appointment == null)
                return null;

            return new AppointmentDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                CreatedAt = appointment.CreatedAt
            };
        }
        public async Task CancelAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            appointment.Status = AppointmentStatus.Cancelled;

            await _repository.UpdateAsync(appointment);
        }
    }
}
