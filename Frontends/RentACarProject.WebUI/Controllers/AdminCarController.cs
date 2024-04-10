using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Dto.CarDtos;

namespace RentACarProject.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Cars/GetCarWithBrand");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            // For Brand Dropdpwn List
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Brands");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
            List<SelectListItem> brandValues = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.BrandID.ToString()
                                                }).ToList();

            // Transmission Type Dropdown List
            List<SelectListItem> transmissionValues = new List<SelectListItem>
            {
                new SelectListItem { Text = "Manual Transmission", Value = "Manual" },
                new SelectListItem { Text = "Automatic Transmission", Value = "Automatic" },
                new SelectListItem { Text = "Semi-Automatic Transmission", Value = "Semi-Automatic" },
                new SelectListItem { Text = "Direct Drive (Fixed Gear)", Value = "Direct Drive" },
                new SelectListItem { Text = "Dual-Clutch Transmission (DSG or PDK)", Value = "Dual-Clutch" }
            };


            // Fuel Type Dropdown List
            List<SelectListItem> fuelTypeValues = new List<SelectListItem>
            {
                new SelectListItem { Text = "Gasoline", Value = "Gasoline" },
                new SelectListItem { Text = "Diesel", Value = "Diesel" },
                new SelectListItem { Text = "Electricity", Value = "Electricity" }
            };

            ViewBag.BrandValues = brandValues;
            ViewBag.TransmissionValues = transmissionValues;
            ViewBag.FuelTypeValues = fuelTypeValues;

            return View();


        }
    }
}
