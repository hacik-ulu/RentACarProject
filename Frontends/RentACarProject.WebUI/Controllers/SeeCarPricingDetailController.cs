using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class SeeCarPricingDetailController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "CAR PRİCİNG DETAİLS ";
            ViewBag.v2 = "Various Car Pricing Details";
            return View();
        }

       
    }
}
