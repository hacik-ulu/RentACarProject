using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class TermConditionController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "TERMS AND CONDITIONS ";
            ViewBag.v2 = "Terms and Conditions ";
            return View();
        }
    }
}
