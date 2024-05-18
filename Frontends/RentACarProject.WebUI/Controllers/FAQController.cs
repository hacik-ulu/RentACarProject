using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class FAQController : Controller
    {
        private List<string> FAQs = new List<string>
        {
            "Soru 1: Cevap 1",
            "Soru 2: Cevap 2",
            "Soru 3: Cevap 3"
        };

        // FAQ View'ı döndüren action
        public async Task<IActionResult> Index()
        {

            ViewBag.v1 = "FAQ ";
            // FAQs listesini View'a gönder
            return View(FAQs);
        }
    }
}
