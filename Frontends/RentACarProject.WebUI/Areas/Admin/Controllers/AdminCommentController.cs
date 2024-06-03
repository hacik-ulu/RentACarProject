using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CommentDtos;
using RentACarProject.Dto.LocationDtos;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminComment")]
    public class AdminCommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminCommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v = id;
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var claims = User.Claims;
                if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                {
                    // Admin ise işlemleri yap ve AdminLocation/Index sayfasına yönlendir
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseMessage = await client.GetAsync("https://localhost:7262/api/Comments/CommentListByBlog?id=" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonData = await responseMessage.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
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
    }
}