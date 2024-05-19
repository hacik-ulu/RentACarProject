using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class FAQController : Controller
    {
        private List<string> FAQs = new List<string>
{
        "Question 1: Where can I find the rental fees and details?",
        "Answer 1: You can find the rental fees and details under the 'Pricing' section. Here, you can see the daily, weekly, or monthly rental rates for the vehicles. Additionally, you can view more details by clicking on 'See Pricing Detail' within the price list.",

        "Question 2: Which brands and models of vehicles does Rapid Rent offer for rental?",
        "Answer 2: Rapid Rent offers a wide range of vehicles from various brands. You can view the available vehicles under the 'Cars' section on our website. We have agreements with many brands to provide you with a diverse selection.",

        "Question 3: Who covers the expenses in case of an accident?",
        "Answer 3: Rapid Rent has agreements with insurance and coverage companies. In the event of an accident, the expenses for both you and the third party are covered by our partner insurance companies.",

        "Question 4: How can I reach you if I have any questions?",
        "Answer 4: You can reach us 24/7 through our call centers, physical offices, or via the 'Contact' section on our website. Our customer service team is always ready to assist you.",

        "Question 5: What other services does your company offer besides car rental?",
        "Answer 5: Besides car rental, we also provide wedding car rentals, city tours, VIP services, and airport transfers. For more information, please visit our website.",

        "Question 6: Where can I find detailed information about Rapid Rent and your company?",
        "Answer 6: You can find detailed information about Rapid Rent and our company in the 'About' section of our website. Here, you can learn about our history, mission, and vision."
};


        // FAQ View'ı döndüren action
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "FAQ ";
            ViewBag.v2 = "Frequently Asked Questions ";
            // FAQs listesini View'a gönder
            return View(FAQs);
        }
    }
}
