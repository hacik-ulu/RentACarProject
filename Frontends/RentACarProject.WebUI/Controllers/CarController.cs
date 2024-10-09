using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CarPricingDtos;

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
			var responseMessage = await client.GetAsync("https://localhost:7262/api/CarPricings");
			if (responseMessage.IsSuccessStatusCode)
			{
				// We are reading data from uotcome of our Api as string format.
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCarPricingWithCarDto>>(jsonData);
				return View(values);
			}
			return View();
		}

        public IActionResult CarDetail(int id)
        {
            ViewBag.v1 = "CAR DETAİLS ";
            ViewBag.v2 = "Car Specifications";
            ViewBag.CarID = id;
            return View();
        }
    }
}
