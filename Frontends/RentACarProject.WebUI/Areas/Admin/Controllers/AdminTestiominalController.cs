using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.LocationDtos;
using RentACarProject.Dto.TestimonialDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminTestimonial")]
    public class AdminTestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminTestimonialController(IHttpClientFactory httpClientFactory)
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
            if (token != null)
            {
                var claims = User.Claims;
                if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                {
                    // Admin ise işlemleri yap ve AdminLocation/Index sayfasına yönlendir
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseMessage = await client.GetAsync("https://localhost:7262/api/Testimonials");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonData = await responseMessage.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);

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
            }
            return View();
        }

        [HttpGet]
        [Route("CreateTestimonial")]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateTestimonial")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7262/api/Testimonials", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
            }
            return View();
        }

        [Route("RemoveTestimonial/{id}")]
        public async Task<IActionResult> RemoveTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7262/api/Testimonials?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var resposenMessage = await client.GetAsync($"https://localhost:7262/api/Testimonials/{id}");
            if (resposenMessage.IsSuccessStatusCode)
            {
                var jsonData = await resposenMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateTestimonialDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateTestimonialDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7262/api/Testimonials/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
            }
            return View();
        }
    }
}