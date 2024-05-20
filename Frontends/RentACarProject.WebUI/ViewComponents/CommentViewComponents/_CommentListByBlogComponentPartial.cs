using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CommentDtos;
using RentACarProject.Dto.TagCloudDtos;

namespace RentACarProject.WebUI.ViewComponents.CommentViewComponents
{
    public class _CommentListByBlogComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CommentListByBlogComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.BlogID = id;
            var client = _httpClientFactory.CreateClient();
            // GetAsync used for list or get the datas.
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/Comments/CommentListByBlog?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                // We are reading data from uotcome of our Api as string format.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
