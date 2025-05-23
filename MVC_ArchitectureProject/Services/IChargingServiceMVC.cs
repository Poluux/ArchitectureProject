using MVC_ArchitectureProject.Models;

namespace MVC_ArchitectureProject.Services
{
    public interface IChargingServiceMVC
    {
        Task<TransactionM> rechargeAccount(TransactionM transactionM);
        Task<string> UpdateBalanceAndQuota(UserBalanceModel userUpdate);
        Task<string> RechargeByCard(CardRechargeModel model);
    }
}
