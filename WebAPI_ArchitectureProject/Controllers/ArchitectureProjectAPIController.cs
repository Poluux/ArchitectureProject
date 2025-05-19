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

        [HttpGet("convertCHFToPage/{amount}")]
        public async Task<ActionResult<int>> GetNbrOfPage(double amount)
        {
            var nbrOfPage = await _quotaConversionService.convertCHFtoPage(amount);
            return Ok(nbrOfPage);
        }

        [HttpPost("SchoolToStudent/")]
        public async Task<ActionResult<TransactionM>> recordTransaction(TransactionM transactionM)
        {
            var transaction = transactionM.ToTransactionDAL();
            var transactionReturned = await _chargingService.PostTransaction(transaction);
            return (transactionReturned.ToTranscationM());
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
    }
}
