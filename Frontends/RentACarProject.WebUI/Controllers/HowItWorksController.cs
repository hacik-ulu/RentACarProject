using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class HowItWorksController : Controller
    {
        public IActionResult Index()
        {
            var howItWorks = new List<string>
            {
                "1. Rent Your Car",
                "Create an application for the vehicle you want to rent by following the instructions on our Website.",

                 "2.Information Controll Process",
                 "Our authorized units will examine your application meticulously and carefully. In this step, the accuracy of the information and the demands of our valued customers will be evaluated.",

                 "3.Check Your Email",
                 "After the evaluation phase is completed, the results will be announced via e-mail by the rental department. After completing your application, you are kindly requested to check your e-mail frequently. Your application will be responded to positively or negatively. We will state our reasons for negative feedback.",

                 "4.Come to Our Office",
                 "If the applications are positive, we will host you in our offices. Important information such as approval of the rental procedure, receipt of fees and completion of signature procedures will be provided in our offices. You are kindly requested to come to our offices in order to deliver the vehicles and obtain accurate information."

            };


            ViewBag.v1 = "HOW IT WORKS ? ";
            ViewBag.v2 = "Rapid Rent Tips";

            return View(howItWorks);
        }
    }
}
