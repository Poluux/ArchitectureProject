namespace WebAPI_ArchitectureProject.Business
{
    public interface IQuotaConversionService
    {
        Task<int> convertCHFtoPage(double amount);
    }
}
