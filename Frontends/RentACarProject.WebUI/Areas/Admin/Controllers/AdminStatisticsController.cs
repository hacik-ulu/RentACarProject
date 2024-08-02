using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.AuthorDtos;
using RentACarProject.Dto.StatisticsDtos;
using System.Security.Claims;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
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

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (token != null)
            {
                var claims = User.Claims;
                if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                {
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

                    var responseMessage8 = await client.GetAsync("https://localhost:7262/api/Statistics/GetAvgRentPriceForHourly");
                    if (responseMessage8.IsSuccessStatusCode)
                    {
                        int avgRentPriceHourlyCountRandom = random.Next(0, 101);
                        var jsonData8 = await responseMessage8.Content.ReadAsStringAsync();
                        var values8 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData8);
                        ViewBag.avgRentPriceHourlyCount = values8.AvgRentPriceForHourly;
                        ViewBag.avgRentPriceHourlyCountRandom = avgRentPriceHourlyCountRandom;
                    }


                    #region S9
                    var responseMessage9 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCountByTranmissionIsAuto");
                    if (responseMessage9.IsSuccessStatusCode)
                    {
                        int CarCountByTransmissionIsAutoRandom = random.Next(0, 101);
                        var jsonData9 = await responseMessage9.Content.ReadAsStringAsync();
                        var values9 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData9);
                        ViewBag.CarCountByTransmissionIsAutoCount = values9.CarCountByTransmissionIsAuto;
                        ViewBag.CarCountByTransmissionIsAutoCountRandom = CarCountByTransmissionIsAutoRandom;
                    }
                    #endregion

                    #region S10
                    var responseMessage10 = await client.GetAsync("https://localhost:7262/api/Statistics/GetBrandNameByMaxCar");
                    if (responseMessage10.IsSuccessStatusCode)
                    {
                        int brandNameByMaxCarRandom = random.Next(0, 101);
                        var jsonData10 = await responseMessage10.Content.ReadAsStringAsync();
                        var values10 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData10);
                        ViewBag.brandNameByMaxCar = values10.BrandNameByMaxCar;
                        ViewBag.brandNameByMaxCarRandom = brandNameByMaxCarRandom;
                    }
                    #endregion

                    #region S11
                    var responseMessage11 = await client.GetAsync("https://localhost:7262/api/Statistics/GetBlogTitleByMaxBlogComment");
                    if (responseMessage11.IsSuccessStatusCode)
                    {
                        int BlogTitleByMaxBlogCommentRandom = random.Next(0, 101);
                        var jsonData11 = await responseMessage11.Content.ReadAsStringAsync();
                        var values11 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData11);
                        ViewBag.BrandNameByMaxCarRandomCount = values11.BlogTitleByMaxBlogComment;
                        ViewBag.BrandNameByMaxCarRandom = BlogTitleByMaxBlogCommentRandom;
                    }
                    #endregion

                    #region S12
                    var responseMessage12 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCountByKmSmallerThen10000");
                    if (responseMessage12.IsSuccessStatusCode)
                    {
                        int CarCountByKmSmallerThen10000Random = random.Next(0, 101);
                        var jsonData12 = await responseMessage12.Content.ReadAsStringAsync();
                        var values12 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData12);
                        ViewBag.CarCountByKmSmallerThen10000 = values12.CarCountByKmSmallerThen10000;
                        ViewBag.CarCountByKmSmallerThen10000Random = CarCountByKmSmallerThen10000Random;
                    }
                    #endregion

                    #region S13
                    var responseMessage13 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCountByFuelGasolineOrDiesel");
                    if (responseMessage13.IsSuccessStatusCode)
                    {
                        int CarCountByFuelGasolineOrDieselRandom = random.Next(0, 101);
                        var jsonData13 = await responseMessage13.Content.ReadAsStringAsync();
                        var values13 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData13);
                        ViewBag.CarCountByFuelGasolineOrDiesel = values13.CarCountByFuelGasolineOrDiesel;
                        ViewBag.CarCountByFuelGasolineOrDieselRandom = CarCountByFuelGasolineOrDieselRandom;
                    }
                    #endregion

                    #region S14
                    var responseMessage14 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarCountByFuelElectric");
                    if (responseMessage14.IsSuccessStatusCode)
                    {
                        int CarCountByFuelElectricRandom = random.Next(0, 101);
                        var jsonData14 = await responseMessage14.Content.ReadAsStringAsync();
                        var values14 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData14);
                        ViewBag.CarCountByFuelElectric = values14.CarCountByFuelElectric;
                        ViewBag.CarCountByFuelElectricRandom = CarCountByFuelElectricRandom;
                    }
                    #endregion

                    #region S15
                    var responseMessage15 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarBrandAndModelByRentPriceDailyMax");
                    if (responseMessage15.IsSuccessStatusCode)
                    {
                        int CarBrandNameAndModelByRentPriceDailyMaxRandom = random.Next(0, 101);
                        var jsonData15 = await responseMessage15.Content.ReadAsStringAsync();
                        var values15 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData15);
                        ViewBag.CarBrandNameAndModelByRentPriceDailyMax = values15.CarBrandNameAndModelByRentPriceDailyMax;
                        ViewBag.CarBrandNameAndModelByRentPriceDailyMaxRandom = CarBrandNameAndModelByRentPriceDailyMaxRandom;
                    }
                    #endregion

                    #region S16
                    var responseMessage16 = await client.GetAsync("https://localhost:7262/api/Statistics/GetCarBrandAndModelByRentPriceDailyMin");
                    if (responseMessage16.IsSuccessStatusCode)
                    {
                        int CarBrandNameAndModelByRentPriceDailyMinRandom = random.Next(0, 101);
                        var jsonData16 = await responseMessage16.Content.ReadAsStringAsync();
                        var values16 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData16);
                        ViewBag.CarBrandNameAndModelByRentPriceDailyMin = values16.CarBrandNameAndModelByRentPriceDailyMin;
                        ViewBag.CarBrandNameAndModelByRentPriceDailyMinRandom = CarBrandNameAndModelByRentPriceDailyMinRandom;
                    }
                    #endregion
                }
                else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
                {
                    return RedirectToAction("Index", "Default");
                }
            }
            return View();
        }
    }
}