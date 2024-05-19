using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.Controllers
{
    public class SecurityController : Controller
    {
        private List<string> security = new List<string>
        {
            "Rapid Rent policy for protection of information",
            "We request your name, phone number and e-mail address from you. These information help us to process your reservation, to contact with you in case you have any problem regarding your requests, or to submit the electronic confirmation of your reservation.",

            "Which information are collected by Rapid Rent?",
            "IP Address: We record your IP address when you visit our website. This address only identifies your Internet Service Provider (ISP) and does not contain any information about you. We use this data in order to monitor the traffic source of our website in a better way.",

            "How does Rapid Rent protect your information?",
            "Our Rapid Rent website is built on AspNet Core technology with Sha256 encryption to store data completely encrypted. JWT authentication is implemented in our system. Payment card information of our users is not stored in the system at all. Information such as TC Identification Number, Email Address, and phone are encrypted.",

            "How does Rapid Rent use cookies?",
            "We use cookies to enhance your experience on our website. Cookies help us understand how you use our site and personalize your experience. You can control the use of cookies through your browser settings.",

            "What are your rights regarding your personal data?",
            "You have the right to access, correct, or delete your personal data. If you wish to exercise these rights, please contact us through our customer service. We are committed to ensuring that your data is protected and handled appropriately."
        };
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "SECURITY ";
            ViewBag.v2 = "Privacy and Data Protection";
            return View(security);
        }
    }
}
