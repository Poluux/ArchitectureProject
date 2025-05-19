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
        public ArchitectureProjectAPIController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
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
    }
}
