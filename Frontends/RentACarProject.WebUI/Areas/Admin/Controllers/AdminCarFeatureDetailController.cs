using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.Sig;
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

        [HttpGet]
        [Route("CreateFeatureByCarId/{carId}")] // Daha önceden hiç kaydı bulunmayan ve Availability özelliği 0 olanları getiriyor.
        public async Task<IActionResult> CreateFeatureByCarId(int carId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Features");

            List<ResultFeatureDto> allFeatures = new List<ResultFeatureDto>();
            List<int> assignedFeatureIds = new List<int>();

            // Tüm özellikleri al
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                allFeatures = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            }

            string connectionString = "Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"; // Veritabanı bağlantı string'iniz

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // SQL sorgusu
                    string sqlQuery = @"
                SELECT cf.FeatureID
                FROM CarFeatures cf
                WHERE cf.CarID = @CarID";

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CarID", carId);

                        // Sorguyu çalıştırıyoruz
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                assignedFeatureIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Availability = 0 olanları ve henüz atanmadıkları özellikleri filtrele
            var filteredFeatures = allFeatures
                .Where(feature => !assignedFeatureIds.Contains(feature.FeatureID)) // Henüz atanmadığı özellikler
                .ToList();

            ViewBag.CarID = carId;

            return View(filteredFeatures);
        }


        [HttpPost]
        [Route("CreateFeatureByCarId/{carId}")]
        public async Task<IActionResult> CreateFeatureByCarId(int carId, List<int> selectedFeatureIds)
        {
            if (selectedFeatureIds == null || !selectedFeatureIds.Any())
            {
                return BadRequest("No features selected.");
            }

            var client = _httpClientFactory.CreateClient();

            // DTO'yu oluştur
            var createFeatureDto = new CreateFeatureByCarIdDto
            {
                CarID = carId,
                FeatureIDs = selectedFeatureIds  // Seçilen FeatureID'leri liste olarak ata
            };

            var jsonContent = JsonConvert.SerializeObject(createFeatureDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // API'yi çağırıyoruz
            var response = await client.PostAsync("https://localhost:7262/api/CarFeatures/CreateCarFeatureByCarID", content);

            if (!response.IsSuccessStatusCode)
            {
                // Hata mesajını içeriğinden okuyalım
                var errorMessage = await response.Content.ReadAsStringAsync();

                // Yanıtın durum kodunu ve içeriğini loglayalım
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Content: {errorMessage}");

                return StatusCode((int)response.StatusCode, $"Failed to create feature for the car. Error: {errorMessage}");
            }



            return RedirectToAction("Index", "AdminCar");
        }













    }
}
