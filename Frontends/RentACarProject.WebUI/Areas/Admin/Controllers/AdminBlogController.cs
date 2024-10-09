using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RentACarProject.Dto.AuthorDtos;
using RentACarProject.Dto.BlogDtos;
using RentACarProject.Dto.CategoryDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBlog")]
    public class AdminBlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogController(IHttpClientFactory httpClientFactory)
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
                    var responseMessage = await client.GetAsync("https://localhost:7262/api/Blogs/GetAllBlogsWithAuthorListForAdminPanel");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonData = await responseMessage.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultAllBlogsWithAuthorDto>>(jsonData);

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
            }
            return View(new List<ResultAllBlogsWithAuthorDto>());
        }

        [Route("CreateBlog")]
        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            var client = _httpClientFactory.CreateClient();

            // Blog verilerini çekme
            var responseMessage1 = await client.GetAsync("https://localhost:7262/api/Blogs");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAllBlogsWithAuthorDto>>(jsonData1);
            var blogValues = values.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.BlogID.ToString()
            }).ToList();

            // Author verilerini çekme
            var responseMessage2 = await client.GetAsync("https://localhost:7262/api/Authors");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(jsonData2);
            var authorValues = values2.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.AuthorID.ToString()
            }).ToList();

            // Category verilerini çekme
            var responseMessage3 = await client.GetAsync("https://localhost:7262/api/Categories");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            var values3 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData3);
            var categoryValues = values3.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryID.ToString()
            }).ToList();

            // ViewBag'i ayarlama
            ViewBag.BlogValues = blogValues;
            ViewBag.AuthorValues = authorValues;
            ViewBag.CategoryValues = categoryValues;

            return View();
        }

        [HttpPost]
        [Route("CreateBlog")]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            if (!ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var responseMessage1 = await client.GetAsync("https://localhost:7262/api/Blogs");
                var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAllBlogsWithAuthorDto>>(jsonData1);
                var blogValues = values.Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.BlogID.ToString()
                }).ToList();

                var responseMessage2 = await client.GetAsync("https://localhost:7262/api/Authors");
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(jsonData2);
                var authorValues = values2.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.AuthorID.ToString()
                }).ToList();

                var responseMessage3 = await client.GetAsync("https://localhost:7262/api/Categories");
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData3);
                var categoryValues = values3.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryID.ToString()
                }).ToList();

                ViewBag.BlogValues = blogValues;
                ViewBag.AuthorValues = authorValues;
                ViewBag.CategoryValues = categoryValues;

                return View(createBlogDto);
            }
            else
            {
                var clientForPost = _httpClientFactory.CreateClient();
                var jsonContent = JsonConvert.SerializeObject(createBlogDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await clientForPost.PostAsync("https://localhost:7262/api/Blogs", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminBlog");
                }
                return View(createBlogDto);
            }

        }

        [Route("RemoveBlog/{id}")]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7262/api/Blogs?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
            }
            return View();
        }
    }
}
