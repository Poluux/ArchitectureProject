using MVC_ArchitectureProject.Models;
using System.Text.Json;

namespace MVC_ArchitectureProject.Services
{
    public class ChargingServiceMVC : IChargingServiceMVC
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ChargingServiceMVC(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["WebAPI:BaseUrl"];
        }

        public async Task<List<TransactionM>> rechargeAccount(List<TransactionM> listTransactionM)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/SchoolToStudent/", listTransactionM);
            response.EnsureSuccessStatusCode();
            var transactionReturned = await response.Content.ReadFromJsonAsync<List<TransactionM>>();
            if (transactionReturned == null)
            {
                throw new Exception("Failed to deserialize TransactionM from the API response.");
            }
            return transactionReturned;

        }

        public async Task<string> UpdateBalanceAndQuota(UserBalanceModel userUpdate)
        {
            var response = await _httpClient.PatchAsJsonAsync(_baseUrl + "/updateBalanceAndQuota/", userUpdate);
            if (response.IsSuccessStatusCode)
            {
                var resultMessage = await response.Content.ReadAsStringAsync();
                return resultMessage;
            } else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $"Error: {errorMessage}";
            }
        }

        public async Task<string> RechargeByCard(CardRechargeModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/rechargeByCard", model);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<UserBalanceModel>> GetAllUserBalance()
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/Users/");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var students = JsonSerializer.Deserialize<List<UserBalanceModel>>(responseBody, options);
            return students;
        }
    }
}
