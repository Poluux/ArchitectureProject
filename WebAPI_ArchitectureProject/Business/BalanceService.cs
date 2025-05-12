using DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ArchitectureProject.Business
{
    public class BalanceService : IBalanceService
    {
        private readonly MS_SQLContext _sqlContext;

        public BalanceService(MS_SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<double?> getUserBalanceAsync(string username)
        {
            return await _sqlContext.Users
                                     .Where(u => u.Username == username)
                                     .Select(u => (double?)u.Balance)
                                     .FirstOrDefaultAsync();
        }
    }
}
