
namespace WebAPI_ArchitectureProject.Business
{
    public class QuotaConversionService : IQuotaConversionService
    {
        private const int ratioCHftoPages = 4; // 1 CHF = 4 pages
        public async Task<int> convertCHFtoPage(double amount)
        {
            return (int)(amount * ratioCHftoPages);
        }
    }
}
