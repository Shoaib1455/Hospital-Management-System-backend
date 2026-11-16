using AppointmentService.DTOs;
using AppointmentService.Events;
using AppointmentService.Interfaces;
using AppointmentService.Models;

namespace AppointmentService.Services
{
    public class AppointmentServices:IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPatientServiceClient _patientClient;
        private readonly IDoctorServiceClient _doctorClient;
        private readonly IEventPublisher _publisher;
        public AppointmentServices(
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
                await _patientClient.PatientExistsAsync(dto.Patientid);

            if (!patientExists)
                throw new Exception("Patient not found.");

            // Verify doctor exists
            var doctor =
                await _doctorClient.GetDoctorAsync(dto.Doctorid);

            if (doctor == null)
                throw new Exception("Doctor not found.");

            // Verify doctor is available
            var slotBooked =
                await _repository.IsSlotBooked(
                    dto.Doctorid,
                    dto.Appointmentdate,
                    dto.Appointmenttime);

            if (slotBooked)
                throw new Exception("Slot already booked.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Patientid = dto.Patientid,
                Doctorid = dto.Doctorid,
                Appointmentdate = dto.Appointmentdate,
                Appointmenttime = dto.Appointmenttime,
                Status = AppointmentStatus.PendingPayment.ToString()
            };

            await _repository.AddAsync(appointment);

            await _publisher.PublishAsync("appointment.created", new AppointmentCreatedEvent
            {
                AppointmentId = appointment.Id,
                Patientid = appointment.Patientid,
                Doctorid = appointment.Doctorid,
                Appointmentdate = appointment.Appointmentdate,
                Appointmenttime = appointment.Appointmenttime,
                ConsultationFee = doctor.ConsultationFee,
                Amount = doctor.ConsultationFee
            });

            return new AppointmentDto
            {
                Id = appointment.Id,
                Patientid = appointment.Patientid,
                Doctorid = appointment.Doctorid,
                Appointmentdate = appointment.Appointmentdate,
                Appointmenttime = appointment.Appointmenttime,
                Status = appointment.Status
            };
        }
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                Patientid = a.Patientid,
                Doctorid = a.Doctorid,
                Appointmentdate = a.Appointmentdate,
                Status = a.Status,
                CreatedAt = a.Createdat
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
                Patientid = appointment.Patientid,
                Doctorid = appointment.Doctorid,
                Appointmentdate = appointment.Appointmentdate,
                Status = appointment.Status,
                CreatedAt = appointment.Createdat
            };
        }
        public async Task CancelAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            appointment.Status = AppointmentStatus.Cancelled.ToString();

            await _repository.UpdateAsync(appointment);
        }
    }
}
