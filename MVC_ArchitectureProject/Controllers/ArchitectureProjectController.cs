using Microsoft.AspNetCore.Mvc;
using MVC_ArchitectureProject.Models;
using MVC_ArchitectureProject.Services;

namespace MVC_ArchitectureProject.Controllers
{
    public class ArchitectureProjectController : Controller
    {
        private IBalanceServiceMVC _balanceServiceMVC;
        private IChargingServiceMVC _chargingServiceMVC;
        private IQuotaConversionServiceMVC _quotaConversionServiceMVC;
        public ArchitectureProjectController(IBalanceServiceMVC balanceServiceMVC, IChargingServiceMVC chargingServiceMVC, IQuotaConversionServiceMVC quotaConversionServiceMVC)
        {
            _balanceServiceMVC = balanceServiceMVC;
            _chargingServiceMVC = chargingServiceMVC;
            _quotaConversionServiceMVC = quotaConversionServiceMVC;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewBalance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewBalance(string username)
        {
            var result = await _balanceServiceMVC.GetBalanceAsync(username);
            return View("ViewBalanceResult", result);
        }

        [HttpGet]
        public async Task<IActionResult> SchoolToStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SchoolToStudent(TransactionM transactionM)
        {
            var transaction = await _chargingServiceMVC.rechargeAccount(transactionM);
            var nbrOfPages = await _quotaConversionServiceMVC.convertCHFtoPage(transactionM.Amount);

            var updateUser = new UserBalanceModel
            {
                Username = transactionM.Username,
                Balance = transactionM.Amount,
                Quota = nbrOfPages
            };

            string updateMessage = await _chargingServiceMVC.UpdateBalanceAndQuota(updateUser);
            var viewModel = new TransactionResultViewModel
            {
                Transaction = transaction,
                Message = updateMessage
            };

            return View("TransactionResult", viewModel);
        }
    }
}
