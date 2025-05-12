using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ArchitectureProject.Business;

namespace WebAPI_ArchitectureProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchitectureProjectAPIController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        public ArchitectureProjectAPIController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("balance/{username}")]
        public async Task<ActionResult<double>> GetUserBalance(string username)
        {
            var balance = await _balanceService.getUserBalanceAsync(username);

            if (balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }
    }
}
