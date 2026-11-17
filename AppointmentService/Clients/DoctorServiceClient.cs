using AppointmentService.DTOs;
using AppointmentService.Interfaces;

namespace AppointmentService.Clients
{
    public class DoctorServiceClient : IDoctorServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DoctorServiceClient> _logger;

        public DoctorServiceClient(HttpClient httpClient, ILogger<DoctorServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> DoctorExistsAsync(Guid Doctorid)
        {
            var doctor = await GetDoctorAsync(Doctorid);
            return doctor != null;
        }

        public async Task<DoctorResponseDto?> GetDoctorAsync(Guid Doctorid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/doctors/{Doctorid}");
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<DoctorResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch doctor {DoctorId}", Doctorid);
                return null;
            }
        }
    }
}
