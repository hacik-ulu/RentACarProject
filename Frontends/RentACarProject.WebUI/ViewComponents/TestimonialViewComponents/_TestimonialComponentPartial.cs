using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.AboutDtos;

namespace RentACarProject.WebUI.ViewComponents.TestimonialViewComponents
{
    public class _TestimonialComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TestimonialComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            // GetAsync used for list or get the datas.
            var responseMessage = await client.GetAsync("https://localhost:7262/api/Testimonials");
            if (responseMessage.IsSuccessStatusCode)
            {
                // We are reading data from uotcome of our Api as string format.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                //return View(values);
            }
            return View();
        }
    }
}
