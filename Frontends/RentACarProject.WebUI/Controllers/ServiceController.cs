using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.ServiceDtos;
using RentACarProject.Dto.TestimonialDtos;

namespace RentACarProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "SERVİCES ";
            ViewBag.v2 = "Our Services";
            return View();
        }
    }
}
