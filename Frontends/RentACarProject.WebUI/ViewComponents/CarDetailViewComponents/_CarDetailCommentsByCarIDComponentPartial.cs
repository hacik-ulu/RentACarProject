using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CarFeatureDtos;
using RentACarProject.Dto.ReviewDtos;

namespace RentACarProject.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCommentsByCarIDComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CarDetailCommentsByCarIDComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.CarID = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Reviews?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReviewByCarIDDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
