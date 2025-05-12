namespace WebAPI_ArchitectureProject.Business
{
    public interface IBalanceService
    {
        Task<double?> getUserBalanceAsync(string username);
    }
}
