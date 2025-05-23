using DataAccessLayer.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WebAPI_ArchitectureProject.Business;

namespace WebAPI_ArchitectureProject.Extension
{
    public static class ConverterExtension
    {
        public static WebAPI_ArchitectureProject.Model.UserBalanceModel ToUserBalanceModel(this DataAccessLayer.Models.User user)
        {
            return new WebAPI_ArchitectureProject.Model.UserBalanceModel
            {
                Username = user.Username,
                Balance = user.Balance,
                Quota = user.Quota,
            };
        }

        public static DataAccessLayer.Models.User toUserDAL(this WebAPI_ArchitectureProject.Model.UserBalanceModel userBalanceModel)
        {
            return new DataAccessLayer.Models.User
            {
                Username = userBalanceModel.Username,
                Balance = userBalanceModel.Balance,
                Quota = userBalanceModel.Quota,
                Transactions = new List<Transaction>()

            };
        }

        public static DataAccessLayer.Models.Transaction ToTransactionDAL(this WebAPI_ArchitectureProject.Model.TransactionM transactionM)
        {
            return new DataAccessLayer.Models.Transaction
            {
                Username = transactionM.Username,
                Amount = transactionM.Amount,
                Date = DateTime.Now
                /*User = new User
                {
                    Username = transactionM.Username,
                    Balance = 0,
                    Quota = 0,
                    IdCard = 0,
                    Transactions = new List<Transaction>()
                }*/
            };
        }

        public static WebAPI_ArchitectureProject.Model.TransactionM ToTranscationM(this DataAccessLayer.Models.Transaction transaction)
        {
            return new WebAPI_ArchitectureProject.Model.TransactionM
            {
                Username = transaction.Username,
                Amount = transaction.Amount,
            };
        }
    }
}
