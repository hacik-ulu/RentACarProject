using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BlogDtos;
using RentACarProject.Dto.CarPricingDtos;
using RentACarProject.Dto.CommentDtos;
using RentACarProject.Dto.LocationDtos;
using System.Text;

namespace RentACarProject.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 3)
        {
            ViewBag.v1 = "BLOG ";
            ViewBag.v2 = "Our Blog";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/Blogs/GetAllBlogsWithAuthorsList?page={page}&pageSize={pageSize}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAllBlogsWithAuthorDto>>(jsonData);

                // Calculate total number of pages
                int totalCount = int.Parse(responseMessage.Headers.GetValues("X-Total-Count").FirstOrDefault());
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                ViewBag.CurrentPage = page;

                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.v1 = "BLOGS ";
            ViewBag.v2 = "Read Our Blogs";
            ViewBag.BlogID = id;

            var client = _httpClientFactory.CreateClient();
            var responseMessage2 = await client.GetAsync($"https://localhost:7262/api/Comments/CommentCountByBlog?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.commentCount = jsonData2;
            return View();

        }

        [HttpGet]
        public PartialViewResult AddComment(int id)
        {
            ViewBag.BlogID = id;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7262/api/Comments/CreateCommentWithMediator", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BlogDetail", "Blog", new { id = createCommentDto.BlogID });
            }

            ViewBag.Error = "Error occured, please try again!";
            return View(createCommentDto);
        }


    }
}
