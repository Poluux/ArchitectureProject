using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public interface IBalanceService
    {
        Task<UserBalanceModel> getUserBalanceAsync(string username);
    }
}
