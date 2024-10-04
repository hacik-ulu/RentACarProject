using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.CarDtos;
using RentACarProject.Dto.CarPricingDtos;
using RentACarProject.Dto.PricingDtos;
using RentACarProject.Persistence.Repositories.CarPricingRepositories;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

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


        #region Eski Post/CreateCarPricing Metod
        //[HttpPost]
        //[Route("CreateCarPricing")]
        //public async Task<IActionResult> CreateCarPricing(CreateCarPricingDto createCarPricingDto)
        //{
        //    if (!ModelState.IsValid) // Model doğrulama
        //    {
        //        // Hatalı durumda dropdown'ları tekrar yükle
        //        var client1 = _httpClientFactory.CreateClient();

        //        // Pricing verilerini alma
        //        var pricingResponse = await client1.GetAsync("https://localhost:7262/api/Pricings");
        //        if (pricingResponse.IsSuccessStatusCode)
        //        {
        //            var pricingJsonData = await pricingResponse.Content.ReadAsStringAsync();
        //            var pricingValues = JsonConvert.DeserializeObject<List<ResultPricingDto>>(pricingJsonData);
        //            List<SelectListItem> pricingSelectList = pricingValues.Select(x => new SelectListItem
        //            {
        //                Text = x.Name,
        //                Value = x.PricingID.ToString()
        //            }).ToList();

        //            ViewBag.PricingValues = pricingSelectList;
        //        }

        //        // CarPricings verilerini alma
        //        var carPricingResponse = await client1.GetAsync("https://localhost:7262/api/Cars/");
        //        if (carPricingResponse.IsSuccessStatusCode)
        //        {
        //            var carPricingJsonData = await carPricingResponse.Content.ReadAsStringAsync();
        //            var carPricingValues = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(carPricingJsonData);

        //            var carSelectList = carPricingValues.Select(x => new SelectListItem
        //            {
        //                Text = $"{x.Model} - {x.Year}",
        //                Value = x.CarID.ToString()
        //            }).ToList();

        //            ViewBag.CarValues = carSelectList;
        //        }

        //        // Model doğrulama hatası durumunda aynı view ile geri dön
        //        return View(createCarPricingDto); // Hata durumunda formu tekrar göster
        //    }

        //    var client = _httpClientFactory.CreateClient();
        //    var jsonData = JsonConvert.SerializeObject(createCarPricingDto);
        //    StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //    var responseMessage = await client.PostAsync("https://localhost:7262/api/CarPricings", stringContent);

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    // Hata durumu
        //    return View(createCarPricingDto); // Hata durumunda formu tekrar göster
        //}
        #endregion

        [HttpPost]
        [Route("CreateCarPricing")]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingDto createCarPricingDto)
        {
            if (!ModelState.IsValid) 
            {
                await LoadDropdowns(); 

                return View(createCarPricingDto); 
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCarPricingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7262/api/CarPricings", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            await LoadDropdowns(); 
            return View(createCarPricingDto);
        }



        [HttpGet]
        [Route("UpdateCarPricing/{carId}")]
        public async Task<IActionResult> UpdateCarPricing(int carId)
        {
            var client = _httpClientFactory.CreateClient();

            // Pricing verilerini alma
            var pricingResponse = await client.GetAsync("https://localhost:7262/api/Pricings");
            if (!pricingResponse.IsSuccessStatusCode)
            {
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

            // Araç modellerini alma
            var carResponse = await client.GetAsync("https://localhost:7262/api/Cars");
            if (!carResponse.IsSuccessStatusCode)
            {
                return BadRequest("Failed to retrieve car data.");
            }

            var carJsonData = await carResponse.Content.ReadAsStringAsync();
            var carValues = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(carJsonData);
            List<SelectListItem> carSelectList = carValues.Select(x => new SelectListItem
            {
                Text = $"{x.Model} - {x.Year}", // Model ve Yılı birleştirerek göster
                Value = x.CarID.ToString()  // CarID
            }).ToList();

            ViewBag.CarValues = carSelectList; // Araç modellerini ViewBag'e ekleyelim

            // Veritabanı bağlantısı için connectionString
            var connectionString = "Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"; // Burada kendi bilgilerinizi girin

            // Belirli CarID'ye ait CarPricings verilerini alma
            List<CarPricing> carPricings = new List<CarPricing>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Asenkron bağlantı açma

                // SQL sorgusu
                string query = "SELECT PricingID, Amount FROM CarPricings WHERE CarID = @CarID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarID", carId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var carPricing = new CarPricing
                            {
                                PricingID = reader.GetInt32(reader.GetOrdinal("PricingID")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                            };
                            carPricings.Add(carPricing);
                        }
                    }
                }
            }

            // CarID ve Amountları içeren model oluşturma
            var updateDto = new UpdateCarPricingDto
            {
                CarID = carId,
                PricingAmounts = carPricings.Select(x => new PricingAmountDto
                {
                    PricingID = x.PricingID,
                    Amount = x.Amount
                }).ToList()
            };

            return View(updateDto);
        }



        [HttpPost]
        [Route("UpdateCarPricing/{id}")]
        public async Task<IActionResult> UpdateCarPricing(UpdateCarPricingDto updateCarPricingDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(updateCarPricingDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCarPricingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7262/api/CarPricings", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            await LoadDropdowns();
            return View(updateCarPricingDto);
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

        private async Task LoadDropdowns()
        {
            var client = _httpClientFactory.CreateClient();

            // Pricing verilerini alma
            var pricingResponse = await client.GetAsync("https://localhost:7262/api/Pricings");
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
            var carPricingResponse = await client.GetAsync("https://localhost:7262/api/Cars/");
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
        }






    }
}