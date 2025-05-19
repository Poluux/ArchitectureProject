using Microsoft.AspNetCore.Mvc;
using MVC_ArchitectureProject.Models;
using MVC_ArchitectureProject.Services;

namespace MVC_ArchitectureProject.Controllers
{
    public class ArchitectureProjectController : Controller
    {
        private IBalanceServiceMVC _balanceServiceMVC;
        public ArchitectureProjectController(IBalanceServiceMVC balanceServiceMVC)
        {
            _balanceServiceMVC = balanceServiceMVC;
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
    }
}
