using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Dto.ContactDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentACarProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminContact")]
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
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
                var responseMessage = await client.GetAsync("https://localhost:7262/api/Contacts");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                    return View(values);
                }

                return View("Error"); 
            }
            else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
            {
                return RedirectToAction("Index", "Default");
            }

            return View(new List<ResultContactDto>());
        }

    }
}
