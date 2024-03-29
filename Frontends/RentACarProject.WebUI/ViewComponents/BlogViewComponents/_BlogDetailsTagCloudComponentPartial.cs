using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BlogDtos;
using RentACarProject.Dto.TagCloudDtos;

namespace RentACarProject.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailsTagCloudComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _BlogDetailsTagCloudComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.BlogID = id;
            var client = _httpClientFactory.CreateClient();
            // GetAsync used for list or get the datas.
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/TagClouds/GetTagCloudByBlogId/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                // We are reading data from uotcome of our Api as string format.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByBlogIdtagCloudDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
