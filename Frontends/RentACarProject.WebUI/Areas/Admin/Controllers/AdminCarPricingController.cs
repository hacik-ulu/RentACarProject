using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CarPricingDtos;
using RentACarProject.Dto.PricingDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACarProject.Dto.CarDtos;
using Microsoft.Data.SqlClient; // SelectListItem için gerekli

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCarPricing")]
    public class AdminCarPricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarPricingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region MethodEski
        //[Route("Index")]
        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        //    if (token == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    var claims = User.Claims;
        //    if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
        //    {
        //        var client = _httpClientFactory.CreateClient();
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var responseMessage = await client.GetAsync("https://localhost:7262/api/CarPricings/GetCarPricingWithTimePeriodList");

        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //            var values = JsonConvert.DeserializeObject<List<ResultCarPricingListWithModelDto>>(jsonData);

        //            // Pagination settings
        //            int pageSize = 5;
        //            int totalRecords = values.Count;
        //            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        //            var paginatedItems = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        //            ViewBag.CurrentPage = page;
        //            ViewBag.TotalPages = totalPages;

        //            return View(paginatedItems);
        //        }
        //    }
        //    else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
        //    {
        //        return RedirectToAction("Index", "Default");
        //    }

        //    return View(new List<ResultCarPricingListWithModelDto>());
        //}
        #endregion


        [Route("Index")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var claims = User.Claims;
            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7262/api/CarPricings/GetCarPricingWithTimePeriodList");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarPricingListWithModelDto>>(jsonData);

                    // CarPricingID'yi almak için veritabanı sorgusu
                    using (var connection = new SqlConnection("Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"))
                    {
                        connection.Open();

                        foreach (var item in values)
                        {
                            var command = new SqlCommand("SELECT CarPricingID FROM CarPricings WHERE CarID = @CarID", connection);
                            command.Parameters.AddWithValue("@CarID", item.CarID);

                            var carPricingId = command.ExecuteScalar();
                            if (carPricingId != null)
                            {
                                // CarPricingID'yi bir değişkene atayabiliriz
                                ViewBag.CarPricingID = (int)carPricingId;
                            }
                        }
                    }

                    // Pagination settings
                    int pageSize = 5;
                    int totalRecords = values.Count;
                    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                    var paginatedItems = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;

                    return View(paginatedItems);
                }
            }
            else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
            {
                return RedirectToAction("Index", "Default");
            }

            return View(new List<ResultCarPricingListWithModelDto>());
        }

        [HttpGet]
        [Route("CreateCarPricing")]
        public async Task<IActionResult> CreateCarPricing()
        {
            var client = _httpClientFactory.CreateClient();

            // Pricing verilerini alma
            var pricingResponse = await client.GetAsync("https://localhost:7262/api/Pricings");
            if (!pricingResponse.IsSuccessStatusCode)
            {
                // Hata durumu
                return BadRequest("Failed to retrieve pricing data.");
            }

            var pricingJsonData = await pricingResponse.Content.ReadAsStringAsync();
            var pricingValues = JsonConvert.DeserializeObject<List<ResultPricingDto>>(pricingJsonData);
            List<SelectListItem> pricingSelectList = pricingValues.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.PricingID.ToString()
            }).ToList();

            ViewBag.PricingValues = pricingSelectList;

            // CarPricings verilerini alma
            var carPricingResponse = await client.GetAsync("https://localhost:7262/api/Cars/");
            if (!carPricingResponse.IsSuccessStatusCode)
            {
                // Hata durumu
                return BadRequest("Failed to retrieve car pricing data.");
            }

            var carPricingJsonData = await carPricingResponse.Content.ReadAsStringAsync();
            var carPricingValues = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(carPricingJsonData);

            // Model isimlerini al ve SelectListItem olarak oluştur
            var carSelectList = carPricingValues.Select(x => new SelectListItem
            {
                Text = $"{x.Model} - {x.Year}", // - Model - Yıl
                Value = x.CarID.ToString()  // CarID
            }).ToList();

            ViewBag.CarValues = carSelectList; // Car modelini ViewBag'e ekleyelim

            return View();
        }

        [HttpPost]
        [Route("CreateCarPricing")]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingDto createCarPricingDto)
        {
            if (!ModelState.IsValid) // Model doğrulama
            {
                // Hatalı durumda dropdown'ları tekrar yükle
                var client1 = _httpClientFactory.CreateClient();

                // Pricing verilerini alma
                var pricingResponse = await client1.GetAsync("https://localhost:7262/api/Pricings");
                if (pricingResponse.IsSuccessStatusCode)
                {
                    var pricingJsonData = await pricingResponse.Content.ReadAsStringAsync();
                    var pricingValues = JsonConvert.DeserializeObject<List<ResultPricingDto>>(pricingJsonData);
                    List<SelectListItem> pricingSelectList = pricingValues.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.PricingID.ToString()
                    }).ToList();

                    ViewBag.PricingValues = pricingSelectList;
                }

                // CarPricings verilerini alma
                var carPricingResponse = await client1.GetAsync("https://localhost:7262/api/Cars/");
                if (carPricingResponse.IsSuccessStatusCode)
                {
                    var carPricingJsonData = await carPricingResponse.Content.ReadAsStringAsync();
                    var carPricingValues = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(carPricingJsonData);

                    var carSelectList = carPricingValues.Select(x => new SelectListItem
                    {
                        Text = $"{x.Model} - {x.Year}",
                        Value = x.CarID.ToString()
                    }).ToList();

                    ViewBag.CarValues = carSelectList;
                }

                // Model doğrulama hatası durumunda aynı view ile geri dön
                return View(createCarPricingDto); // Hata durumunda formu tekrar göster
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCarPricingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7262/api/CarPricings", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Hata durumu
            return View(createCarPricingDto); // Hata durumunda formu tekrar göster
        }

        [Route("RemoveCarPricing/{id}")]
        public async Task<IActionResult> RemoveCarPricing(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7262/api/CarPricings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




    }
}