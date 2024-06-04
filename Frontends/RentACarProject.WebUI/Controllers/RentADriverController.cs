using Microsoft.AspNetCore.Mvc;
using RentACarProject.WebUI.Models;

namespace RentACarProject.WebUI.Controllers
{
    public class RentADriverController : Controller
    {
        public IActionResult RentDriver()
        {
            ViewBag.v1 = "RENT A DRİVER ";
            ViewBag.v2 = "Our Drivers";
            List<DriverModel> drivers = new List<DriverModel>()
            {
                new DriverModel() { FirstName = "Ali", LastName = "Öztürk", Age = 35, Experience = 10, Email = "aliozturk@gmail.com", CarBrand = "Ford", CarModel = "Focus" },
                new DriverModel() { FirstName = "Osman", LastName = "Alp", Age = 28, Experience = 8, Email = "osmanalp@gmail.com", CarBrand = "Toyota", CarModel = "Corolla" },
                new DriverModel() { FirstName = "Ferit", LastName = "Demir", Age = 42, Experience = 15, Email = "demirferit@gmail.com", CarBrand = "Fiat", CarModel = "Egea" },
                new DriverModel() { FirstName = "Ahmet", LastName = "Özalp", Age = 32, Experience = 12, Email = "ahmetozalp@gmail.com", CarBrand = "Hyundai", CarModel = "Accent" },
                new DriverModel() { FirstName = "Talha", LastName = "Çoban", Age = 50, Experience = 20, Email = "talhacoban@gmail.com", CarBrand = "Renault", CarModel = "Clio" },
                new DriverModel() { FirstName = "Ferdi", LastName = "Demirel", Age = 28, Experience = 7, Email = "ferdidemirel@gmail.com", CarBrand = "Renault", CarModel = "Clio" }
            };

            return View(drivers);
        }
    }
}
