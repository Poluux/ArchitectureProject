using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_ArchitectureProject.Extension;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public class BalanceService : IBalanceService
    {
        private readonly MS_SQLContext _sqlContext;

        public BalanceService(MS_SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<User> getUserAsync(string username)
        {
            return await _sqlContext.Users
                                     .Where(u => u.Username == username)
                                     .Select(u => u)
                                     .FirstOrDefaultAsync();
        }
    }
}
