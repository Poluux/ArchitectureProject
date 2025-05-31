
using System.Text.Json;

namespace MVC_ArchitectureProject.Services
{
    public class QuotaConversionServiceMVC : IQuotaConversionServiceMVC
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7036/api/ArchitectureProjectAPI";

        public QuotaConversionServiceMVC(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["WebAPI:BaseUrl"];
        }

        public async Task<int> convertCHFtoPage(double amount)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/convertCHFtoPage/" + amount);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var nbrOfPage = JsonSerializer.Deserialize<int>(responseBody);
            return nbrOfPage;
        }
    }
}
