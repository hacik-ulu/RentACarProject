using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.AuthorDtos;
using RentACarProject.Dto.StatisticsDtos;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminStatistics")]
    public class AdminStatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminStatisticsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            Random random = new Random();
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                int v1 = random.Next(0, 101);
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                ViewBag.v = values.CarCount;
                ViewBag.v1 = v1;
            }

            var responseMessage2 = await client.GetAsync("https://localhost:7262/api/Statistics/GetLocationCount");
            if (responseMessage2.IsSuccessStatusCode)
            {
                // Progress bar ve yükselme/alçalma oranı için random değerler atadık ama sayı için direkt apiden gelen veri atandı.
                int locationCountRandom = random.Next(0, 101);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
                ViewBag.locationCount = values2.LocationCount;
                ViewBag.locationCountRandom = locationCountRandom;
            }
            return View();
        }
    }
}
