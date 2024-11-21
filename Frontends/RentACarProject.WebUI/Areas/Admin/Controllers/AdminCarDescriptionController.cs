using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RentACarProject.Dto.BannerDtos;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Dto.CarDescriptionDtos;
using RentACarProject.Dto.CarDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web.WebPages.Html;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCarDescription")]
    public class AdminCarDescriptionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminCarDescriptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

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

                var carDescriptions = new List<ResultCarDescriptionDto>();

                using (var connection = new SqlConnection("Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"))
                {
                    await connection.OpenAsync();

                    string query = @"
                SELECT 
                    cd.CarDescriptionID,
                    c.CarID,
                    CONCAT(b.Name, ' ', c.Model, ' (', c.Year, ')') AS Details
                FROM 
                    CarDescriptions cd
                JOIN 
                    Cars c ON cd.CarID = c.CarID
                JOIN 
                    Brands b ON c.BrandID = b.BrandID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carDescription = new ResultCarDescriptionDto
                                {
                                    CarDescriptionID = reader.GetInt32(0),
                                    CarID = reader.GetInt32(1),
                                    CarDetails = new List<string> { reader.GetString(2) }
                                };
                                carDescriptions.Add(carDescription);
                            }
                        }
                    }
                }

                int pageSize = 5;
                int totalRecords = carDescriptions.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var paginatedItems = carDescriptions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(paginatedItems);
            }
            else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }

        [HttpGet]
        [Route("CreateCarDescription")]
        public IActionResult CreateCarDescription()
        {
            ViewBag.Cars = GetCarList();
            return View();
        }

        [HttpPost]
        [Route("CreateCarDescription")]
        public async Task<IActionResult> CreateCarDescription(CreateCarDescriptionDto createCarDescriptionDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createCarDescriptionDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7262/api/CarDescriptions", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Failed to create car description.");
            }

            ViewBag.Cars = GetCarList();

            return View(createCarDescriptionDto);
        }

        [HttpGet]
        [Route("UpdateCarDescription/{id}")]
        public async Task<IActionResult> UpdateCarDescription(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7262/api/CarDescriptions/GetCarDescriptionsByCarID?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCarDescriptionDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateCarDescription/{id}")]
        public async Task<IActionResult> UpdateCarDescription(UpdateCarDescriptionDto updateCarDescriptionDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCarDescriptionDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7262/api/CarDescriptions/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCarDescription");
            }
            return View();
        }

        [Route("RemoveCarDescription/{id}")]
        public async Task<IActionResult> RemoveCarDescription(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7262/api/CarDescriptions/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCarDescription");
            }
            return View();
        }




















        private List<SelectListItem> GetCarList()
        {
            var carList = new List<SelectListItem>();

            using (var connection = new SqlConnection("Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"))
            {
                connection.Open();
                string query = @"
            SELECT c.CarID, CONCAT(b.Name, ' ', c.Model, ' (', c.Year, ')') AS CarDetails
            FROM Cars c
            JOIN Brands b ON c.BrandID = b.BrandID";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carList.Add(new SelectListItem
                            {
                                Value = reader.GetInt32(0).ToString(),
                                Text = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return carList;
        }


    }
}


