using MVC_ArchitectureProject.Models;
using System.Text.Json;

namespace MVC_ArchitectureProject.Services
{
    public class BalanceServiceMVC : IBalanceServiceMVC
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7036/api/ArchitectureProjectAPI";

        public BalanceServiceMVC(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
