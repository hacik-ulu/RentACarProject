using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "PAYMENT ";
            ViewBag.v2 = "Payment Option";

            var paymentOption = new List<string>
            {
                "Physical Office Payments",
                "You can easily make your rental payments by coming to our offices in our relevant centers.",

                 "IBAN and Wire Transfer Payments",
                 "By contacting our authorized units, IBAN and money transfer information will be shared with you in parallel with your e-mail information regarding the rental, if the information matches.",

                 "Payment with Reference Number",
                 "You can have the chance to win a free ride if you send your reference number via e-mail or by meeting our authorized units face to face. Important notice: The reference number is not valid for every car, you will have free rental access to suitable vehicles as a result of the e-mail you receive or as directed by our officials."

            };

            return View(paymentOption);
        }
    }
}
