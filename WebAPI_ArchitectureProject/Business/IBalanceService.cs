using DataAccessLayer.Models;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public interface IBalanceService
    {
        Task<User> FetchBalanceByUsername(string username);
        Task<User> GetUsernameByCardId(int cardId);

    }
}
