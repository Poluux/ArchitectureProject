using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ArchitectureProject.Business
{
    public class ChargingService : IChargingService
    {
        private readonly PaymentContext _paymentContext;
        private readonly MS_SQLContext _sqlContext;

        public ChargingService(PaymentContext paymentContext, MS_SQLContext sqlContext)
        {
            _paymentContext = paymentContext;
            _sqlContext = sqlContext;
        }

        public async Task<Transaction> PostTransaction(Transaction transaction)
        {
            _paymentContext.Transactions.Add(transaction);
            await _paymentContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<string> UpdateBalanceAndQuota(User userUpdate)
        {
            var user = await _sqlContext.Users.FirstOrDefaultAsync(u => u.Username == userUpdate.Username);
            if (user == null)
            {
                return "User not found";
            }

            user.Balance += userUpdate.Balance;
            user.Quota += userUpdate.Quota;

            try
            {
                _sqlContext.SaveChangesAsync();
                return "Update successfull";
            } catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    }
}
