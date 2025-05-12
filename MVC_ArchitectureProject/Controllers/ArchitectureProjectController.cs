using Microsoft.AspNetCore.Mvc;

namespace MVC_ArchitectureProject.Controllers
{
    public class ArchitectureProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
