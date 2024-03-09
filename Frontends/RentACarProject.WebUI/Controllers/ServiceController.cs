using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.ServiceDtos;
using RentACarProject.Dto.TestimonialDtos;

namespace RentACarProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            // GetAsync used for list or get the datas.
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Services");
            if (responseMessage.IsSuccessStatusCode)
            {
                // We are reading data from uotcome of our Api as string format.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
