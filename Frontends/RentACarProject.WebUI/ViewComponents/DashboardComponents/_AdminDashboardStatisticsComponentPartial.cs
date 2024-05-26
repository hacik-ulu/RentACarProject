using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.StatisticsDtos;

namespace RentACarProject.WebUI.ViewComponents.DashboardComponents
{
    public class _AdminDashboardStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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
            var responseMessage3 = await client.GetAsync("https://localhost:7262/api/Statistics/GetBrandCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                int brandCountRandom = random.Next(0, 101);
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
                ViewBag.brandCount = values3.BrandCount;
                ViewBag.brandCountRandom = brandCountRandom;
            }
            #endregion

            #region S4
            var responseMessage4 = await client.GetAsync("https://localhost:7262/api/Statistics/GetAvgRentPriceForPerDay");
            if (responseMessage4.IsSuccessStatusCode)
            {
                int avgRentPriceDailyCountRandom = random.Next(0, 101);
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
                ViewBag.avgRentPriceDailyCount = values4.AvgPriceForDaily.ToString("0.00"); ;
                ViewBag.avgRentPriceDailyCountRandom = avgRentPriceDailyCountRandom;
            }
            #endregion

            return View();
        }
    }
}