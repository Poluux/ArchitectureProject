using MVC_ArchitectureProject.Models;

namespace MVC_ArchitectureProject.Services
{
    public class ChargingServiceMVC : IChargingServiceMVC
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7036/api/ArchitectureProjectAPI";

        public ChargingServiceMVC(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TransactionM> rechargeAccount(TransactionM transactionM)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/SchoolToStudent/", transactionM);
            response.EnsureSuccessStatusCode();
            var transactionReturned = await response.Content.ReadFromJsonAsync<TransactionM>();
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
    }
}
