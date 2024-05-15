using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class CarPricingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "PRICING ";
            ViewBag.v2 = "Pricing";
            return View();
        }
    }
}
