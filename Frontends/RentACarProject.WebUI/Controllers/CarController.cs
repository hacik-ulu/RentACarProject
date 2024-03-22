using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CarDtos;

namespace RentACarProject.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "CARS ";
            ViewBag.v2 = "Choose Your Car";

            var client = _httpClientFactory.CreateClient();
            // GetAsync used for list or get the datas.
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Cars/GetCarWithBrand");
            if (responseMessage.IsSuccessStatusCode)
            {
                // We are reading data from uotcome of our Api as string format.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
