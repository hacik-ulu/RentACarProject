using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands;
using RentACarProject.Dto.BlogDtos;
using RentACarProject.Dto.CarDtos;
using RentACarProject.Dto.CarFeatureDtos;
using RentACarProject.Dto.FeatureDtos;
using RentACarProject.Dto.LocationDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCarFeatureDetail")]
    public class AdminCarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")] // Özelliklerin getirilmesi
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
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
                    // Admin ise işlemleri yap ve AdminLocation/Index sayfasına yönlendir
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseMessage = await client.GetAsync("https://localhost:7262/api/CarFeatures?id=" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonData = await responseMessage.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
                        return View(values);
                    }
                }
                else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
                {
                    return RedirectToAction("Index", "Default");
                }
            }
            return View();
        }

        [HttpPost]
        [Route("Index/{id}")] // Özelliklerin checkbox/var olan özelliğin aktif/pasif kontrolünün post edilmesi.)
        public async Task<IActionResult> Index(List<ResultCarFeatureByCarIdDto> resultCarFeatureByCarIdDto)
        {

            foreach (var item in resultCarFeatureByCarIdDto)
            {
                if (item.Availability)
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("https://localhost:7262/api/CarFeatures/CarFeatureChangeAvailableToTrue?id=" + item.CarFeatureID);

                }
                else
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("https://localhost:7262/api/CarFeatures/CarFeatureChangeAvailableToFalse?id=" + item.CarFeatureID);
                }
            }
            return RedirectToAction("Index", "AdminCar");
        }


        [HttpGet("CreateFeatureByCarId/{carId}")]
        public async Task<IActionResult> CreateFeatureByCarId(int carId, int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            // Fetch all features from the API
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Features");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);

                // Filter features for the specific carId

                // Pagination settings
                int pageSize = 5;
                int totalRecords = values.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var paginatedItems = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.CarID = carId;
                return View(paginatedItems); // Pass the paginated and filtered features to the view
            }

            // Optionally handle errors or return an empty view
            return View(new List<ResultFeatureDto>()); // Return an empty list if the call fails
        }


    }
}
