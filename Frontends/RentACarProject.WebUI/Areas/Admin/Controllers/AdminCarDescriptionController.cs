using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RentACarProject.Dto.BannerDtos;
using RentACarProject.Dto.CarDescriptionDtos;
using RentACarProject.Dto.CarDtos;
using System.Net.Http.Headers;
using System.Security.Claims;

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



    }
}


// token olayını metod yapailiriz.