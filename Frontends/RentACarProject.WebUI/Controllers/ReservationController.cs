using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "CAR RENTAL ";
            ViewBag.v2 = "Car Rental Form";
            return View();
        }
    }
}
