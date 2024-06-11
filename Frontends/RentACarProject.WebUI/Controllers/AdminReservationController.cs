using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BlogDtos;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using RentACarProject.Dto.ReservationDtos;

namespace RentACarProject.WebUI.Controllers
{
    public class AdminReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
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
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseMessage = await client.GetAsync("https://localhost:7262/api/Reservations/GetReservationList");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonData = await responseMessage.Content.ReadAsStringAsync();
                        var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
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
