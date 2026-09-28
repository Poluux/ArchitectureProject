using MVC_ArchitectureProject.Models;
using System.Text.Json;

namespace MVC_ArchitectureProject.Services
{
    public class BalanceServiceMVC : IBalanceServiceMVC
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public BalanceServiceMVC(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["WebAPI:BaseUrl"];
        }

        public async Task<UserBalanceModel> GetBalanceAsync(string username)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/balance/" + username);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var userBalanceModel = JsonSerializer.Deserialize<UserBalanceModel>(responseBody, options);
            if (userBalanceModel == null)
            {
                throw new Exception("Failed to deserialize user balance model.");
            }
            return userBalanceModel;
        }
    }
}
