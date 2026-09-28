
namespace WebAPI_ArchitectureProject.Business
{
    public class QuotaConversionService : IQuotaConversionService
    {
        private const double ratioCHftoPages = 0.08; // 0.08 CHF = 1 pages
        public async Task<int> convertCHFtoPage(double amount)
        {
            return await Task.FromResult((int)(amount / ratioCHftoPages));
        }
    }
}
