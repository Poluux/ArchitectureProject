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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewBalance()
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
        public async Task<IActionResult> SchoolToStudentList()
        {
            var students = await _chargingServiceMVC.GetAllUserBalance();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> SchoolToStudent(List<string> selectedUsernames, double amount)
        {
            if (selectedUsernames == null || selectedUsernames.Count == 0 || amount <= 0)
            {
                ModelState.AddModelError("", "Sélectionnez au moins un étudiant et un montant valide.");
                var students = await _chargingServiceMVC.GetAllUserBalance();
                return View("SchoolToStudentList", students);
            }

            var transactions = selectedUsernames.Select(username => new TransactionM
            {
                Username = username,
                Amount = amount
            }).ToList();

            var transactionResult = await _chargingServiceMVC.rechargeAccount(transactions);

            var resultsViewModel = new List<TransactionResultViewModel>();

            foreach (var transaction in transactionResult)
            {
                var pages = await _quotaConversionServiceMVC.convertCHFtoPage(transaction.Amount);

                var updateUser = new UserBalanceModel
                {
                    Username = transaction.Username,
                    Balance = transaction.Amount,
                    Quota = pages
                };

                string updateMessage = await _chargingServiceMVC.UpdateBalanceAndQuota(updateUser);

                var viewModel = new TransactionResultViewModel
                {
                    Transaction = transaction,
                    Message = updateMessage
                };

                resultsViewModel.Add(viewModel);
            }

            return View("TransactionResult", resultsViewModel);
            /*    var transaction = await _chargingServiceMVC.rechargeAccount(transactionM);
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

                return View("TransactionResult", viewModel);    */
        }

        [HttpGet]
        public IActionResult RechargeByCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RechargeByCard(CardRechargeModel model)
        {
            var message = await _chargingServiceMVC.RechargeByCard(model);
            ViewBag.Message = message;
            return View();
        }

    }
}
