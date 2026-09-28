namespace MVC_ArchitectureProject.Services
{
    public interface IQuotaConversionServiceMVC
    {
        Task<int> convertCHFtoPage(double amount);
    }
}
