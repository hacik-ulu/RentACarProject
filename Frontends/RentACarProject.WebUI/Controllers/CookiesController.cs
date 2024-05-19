using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarProject.WebUI.Controllers
{
    public class CookiesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "PRIVACY";
            ViewBag.v2 = "Privacy & Cookies Policy";

            var cookieInfo = new List<string>
            {
                "What are cookies?",
                "Cookies on the internet are not the delicious kind you might be thinking of! Instead, they are small text files that websites store on your device. These files contain information about your visit that can be used to improve your browsing experience.",

                "Why we use cookies?",
                "Cookies play a vital role in the smooth operation of many websites you encounter. They act like little memory chips for websites, remembering information about your visit to make things more convenient and enjoyable for you. For instance, cookies can save you the hassle of re-entering login details or remembering what you added to your shopping cart. They can even personalize your experience by remembering your preferences like language or font size.",

                "Managing Cookies: Your Guide to a Personalized Experience",
                "Cookies are an integral part of the modern web experience, providing websites with the ability to remember your preferences, enhance your browsing experience, and tailor content to your interests. While cookies offer numerous benefits, it's crucial to understand how they work and how you can manage them to suit your privacy preferences.",

                "What do those cookies and tags do?",
                @"We have categorised the cookies and tags on the Avis website as follows:
                <table>
                    <tr>
                        <th>Category</th>
                        <th>Purpose</th>
                    </tr>
                    <tr>
                        <td>Essential Cookies</td>
                        <td>These cookies are essential for the basic functionality of our website. They enable features like user authentication, account management, and language preferences.</td>
                    </tr>
                    <tr>
                        <td>Analytics</td>
                        <td>Analytics cookies collect information about how users interact with our website. This data helps us understand and improve the performance of our site, and identify areas for optimization.</td>
                    </tr>
                    <tr>
                        <td>Advertising and retargeting</td>
                        <td>Cookies in this category personalize your online advertising experience. They track your browsing habits and preferences to display targeted ads based on your interests, both on our site and on other websites.</td>
                    </tr>
                    <tr>
                        <td>Optimisation</td>
                        <td>Optimization cookies store information to enhance your experience on our website. They remember your preferences, such as language or region selection, to customize your browsing experience.</td>
                    </tr>
                    <tr>
                        <td>Preferences cookie</td>
                        <td>Preferences cookies remember your cookie settings between visits to our site, ensuring that our cookie banner and preference center do not appear every time you visit.</td>
                    </tr>
                </table>"
            };

            return View(cookieInfo);
        }
    }
}
