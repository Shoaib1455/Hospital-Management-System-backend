using AppointmentService.Interfaces;

namespace AppointmentService.Clients
{
    public class PatientServiceClient : IPatientServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PatientServiceClient> _logger;

        public PatientServiceClient(HttpClient httpClient, ILogger<PatientServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> PatientExistsAsync(Guid Patientid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Patient/{Patientid}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to verify patient {PatientId}", Patientid);
                return false;
            }
        }
    }
}
