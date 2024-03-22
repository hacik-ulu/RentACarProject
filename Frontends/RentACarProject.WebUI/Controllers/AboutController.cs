using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "ABOUT US ";
            ViewBag.v2 = "About Us";
            return View();
        }
    }
}
