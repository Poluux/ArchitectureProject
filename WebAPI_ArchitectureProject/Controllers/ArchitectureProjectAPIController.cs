using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ArchitectureProject.Business;
using WebAPI_ArchitectureProject.Extension;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchitectureProjectAPIController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        private readonly IChargingService _chargingService;
        private readonly IQuotaConversionService _quotaConversionService;
        public ArchitectureProjectAPIController(IBalanceService balanceService, IChargingService chargingService, IQuotaConversionService quotaConversionService)
        {
            _balanceService = balanceService;
            _chargingService = chargingService;
            _quotaConversionService = quotaConversionService;
        }

        [HttpGet("balance/{username}")]
        public async Task<ActionResult<UserBalanceModel>> GetUserBalance(string username)
        {
            var user = await _balanceService.FetchBalanceByUsername(username);

            if (user == null)
            {
                return NotFound();
            }

            var userBalanceModel = user.ToUserBalanceModel();

            return Ok(userBalanceModel);
        }

        [HttpGet("Users/")]
        public async Task<ActionResult<IEnumerable<UserBalanceModel>>> GetAllUserBalance()
        {
            var students = await _chargingService.GetAllUserBalance();

            if (students == null)
            {
                return NotFound();
            }

            var listOfStudentsM = new List<UserBalanceModel>();
            foreach (var student in students)
            {
                UserBalanceModel studentM = new UserBalanceModel();
                studentM = student.ToUserBalanceModel();
                listOfStudentsM.Add(studentM);
            }
           
            return Ok(listOfStudentsM);
        }

        [HttpGet("convertCHFToPage/{amount}")]
        public async Task<ActionResult<int>> GetNbrOfPage(double amount)
        {
            var nbrOfPage = await _quotaConversionService.convertCHFtoPage(amount);
            return Ok(nbrOfPage);
        }

        [HttpPost("SchoolToStudent/")]
        public async Task<ActionResult<List<TransactionM>>> recordTransaction(List<TransactionM> listTransactionM)
        {
            if (listTransactionM == null || !listTransactionM.Any())
            {
                return BadRequest("La liste des transactions est vide.");
            }

            var dalTransactions = listTransactionM
                .Select(t => t.ToTransactionDAL())
                .ToList();

            var listTransactionReturned = await _chargingService.PostTransactionList(dalTransactions);

            var result = listTransactionReturned
                .Select(t => t.ToTranscationM())
                .ToList();

            return (result);
        }

        [HttpPatch("updateBalanceAndQuota/")]
        public async Task<ActionResult> PatchUser(UserBalanceModel userUpdateM)
        {
            var userUpdate = userUpdateM.toUserDAL();
            var result = await _chargingService.UpdateBalanceAndQuota(userUpdate);
            if (result == "Update successfull")
            {
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("rechargeAccountByCard")]
        public async Task<ActionResult<string>> RechargeAccountByCard(CardRechargeModel model)
        {
            var username = await _chargingService.GetUsernameByCardId(model.CardId);
            if (username == null) return NotFound("Card not linked.");

            var transaction = new TransactionM
            {
                Username = username,
                Amount = model.Amount
            };

            await _chargingService.PostTransaction(transaction.ToTransactionDAL());
            var pages = await _quotaConversionService.convertCHFtoPage(model.Amount);

            var userUpdate = new UserBalanceModel
            {
                Username = username,
                Balance = model.Amount,
                Quota = pages
            };

            await _chargingService.UpdateBalanceAndQuota(userUpdate.toUserDAL());

            return Ok("Recharge completed successfully.");
        }

        [HttpPost("RechargeByCard")]
        public async Task<IActionResult> RechargeByCard(CardRechargeModel model)
        {
            var result = await _chargingService.RechargeByCard(model);
            return Ok(new { message = result });
        }


    }
}
