using DataAccessLayer.Models;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public interface IChargingService
    {
        Task<Transaction> PostTransaction(Transaction transaction);
        Task<string> UpdateBalanceAndQuota(User userUpdate);
    }
}
