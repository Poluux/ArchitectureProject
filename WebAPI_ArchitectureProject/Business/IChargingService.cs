using DataAccessLayer;
using DataAccessLayer.Models;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public interface IChargingService
    {
        Task<List<Transaction>> PostTransactionList(List<Transaction> listTransaction);
        Task<Transaction> PostTransaction(Transaction transaction);
        Task<string> UpdateBalanceAndQuota(User userUpdate);
        Task<string?> GetUsernameByCardId(int cardId);
        Task<string> RechargeByCard(CardRechargeModel model);
        Task<IEnumerable<User>> GetAllUserBalance();
    }
}
