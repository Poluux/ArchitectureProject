using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public class ChargingService : IChargingService
    {
        private readonly PaymentContext _paymentContext;
        private readonly MS_SQLContext _sqlContext;
        private readonly IQuotaConversionService _quotaConversionService;

        public ChargingService(PaymentContext paymentContext, MS_SQLContext sqlContext, IQuotaConversionService quotaConversionService)
        {
            _paymentContext = paymentContext;
            _sqlContext = sqlContext;
            _quotaConversionService = quotaConversionService;
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
                await _sqlContext.SaveChangesAsync();
                return "Update successfull";
            } catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }

        public async Task<string?> GetUsernameByCardId(int cardId)
        {
            var user = await _sqlContext.Users.FirstOrDefaultAsync(u => u.IdCard == cardId);
            return user?.Username;
        }

        public async Task<string> RechargeByCard(CardRechargeModel model)
        {
            var username = await GetUsernameByCardId(model.CardId);
            if (username == null) return "Card not linked";

            var user = await _sqlContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return "User not found";

            var transaction = new Transaction
            {
                Username = username,
                Amount = model.Amount,
                Date = DateTime.Now,
                //UserId = user.UserId
            };

            _paymentContext.Transactions.Add(transaction);
            await _paymentContext.SaveChangesAsync();

            var pages = await _quotaConversionService.convertCHFtoPage(model.Amount);

            user.Balance += model.Amount;
            user.Quota += pages;

            await _sqlContext.SaveChangesAsync();

            return "Recharge completed successfully";
        }



    }
}
