using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class SeeCarPricingDetailController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "CAR PRİCİNG DETAİLS ";
            ViewBag.v2 = "Rental Packages Details";
            return View();
        }

       
    }
}
