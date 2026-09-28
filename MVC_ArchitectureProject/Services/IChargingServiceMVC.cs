using MVC_ArchitectureProject.Models;

namespace MVC_ArchitectureProject.Services
{
    public interface IChargingServiceMVC
    {
        Task<List<TransactionM>> rechargeAccount(List<TransactionM> listTransactionM);
        Task<string> UpdateBalanceAndQuota(UserBalanceModel userUpdate);
        Task<string> RechargeByCard(CardRechargeModel model);
        Task<List<UserBalanceModel>> GetAllUserBalance();
    }
}
