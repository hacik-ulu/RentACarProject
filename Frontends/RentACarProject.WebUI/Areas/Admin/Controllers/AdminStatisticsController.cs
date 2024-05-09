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

            #region S1
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                int v1 = random.Next(0, 101);
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                ViewBag.v = values.CarCount;
                ViewBag.v1 = v1;
            }
            #endregion

            #region S2
            var responseMessage2 = await client.GetAsync("https://localhost:7262/api/Statistics/GetLocationCount");
            if (responseMessage2.IsSuccessStatusCode)
            {
                int locationCountRandom = random.Next(0, 101);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
                ViewBag.locationCount = values2.LocationCount;
                ViewBag.locationCountRandom = locationCountRandom;
            }
            #endregion

            #region S3
            var responseMessage3 = await client.GetAsync("https://localhost:7262/api/Statistics/GetAuthorCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                int authorCountRandom = random.Next(0, 101);
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
                ViewBag.authorCount = values3.AuthorCount;
                ViewBag.authorCountRandom = authorCountRandom;
            }
            #endregion

            #region S4
            var responseMessage4 = await client.GetAsync("https://localhost:7262/api/Statistics/GetBlogCount");
            if (responseMessage4.IsSuccessStatusCode)
            {
                int blogCountRandom = random.Next(0, 101);
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
                ViewBag.blogCount = values4.BlogCount;
                ViewBag.blogCountRandom = blogCountRandom;
            }
            #endregion

            #region S5
            var responseMessage5 = await client.GetAsync("https://localhost:7262/api/Statistics/GetBrandCount");
            if (responseMessage5.IsSuccessStatusCode)
            {
                int brandCountRandom = random.Next(0, 101);
                var jsonData5 = await responseMessage5.Content.ReadAsStringAsync();
                var values5 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData5);
                ViewBag.brandCount = values5.BrandCount;
                ViewBag.brandCountRandom = brandCountRandom;
            }
            #endregion

            #region S6
            var responseMessage6 = await client.GetAsync("https://localhost:7262/api/Statistics/GetAvgRentPriceForPerDay");
            if (responseMessage6.IsSuccessStatusCode)
            {
                int avgRentPriceDailyCountRandom = random.Next(0, 101);
                var jsonData6 = await responseMessage6.Content.ReadAsStringAsync();
                var values6 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData6);
                ViewBag.avgRentPriceDailyCount = values6.AvgPriceForDaily;
                ViewBag.avgRentPriceDailyCountRandom = avgRentPriceDailyCountRandom;
            }
            #endregion

            #region S7
            var responseMessage7 = await client.GetAsync("https://localhost:7262/api/Statistics/GetAvgRentPriceForWeekly");
            if (responseMessage7.IsSuccessStatusCode)
            {
                int avgRentPriceWeeklyCountRandom = random.Next(0, 101);
                var jsonData7 = await responseMessage7.Content.ReadAsStringAsync();
                var values7 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData7);
                ViewBag.avgRentPriceWeeklyCount = values7.AvgRentPriceForWeekly;
                ViewBag.avgRentPriceWeeklyCountRandom = avgRentPriceWeeklyCountRandom;
            }
            #endregion










            return View();



        }
    }
}