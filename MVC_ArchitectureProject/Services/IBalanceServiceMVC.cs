using MVC_ArchitectureProject.Models;

namespace MVC_ArchitectureProject.Services
{
    public interface IBalanceServiceMVC
    {
        Task<UserBalanceModel> GetBalanceAsync(string username);
    }
}
