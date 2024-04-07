using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
