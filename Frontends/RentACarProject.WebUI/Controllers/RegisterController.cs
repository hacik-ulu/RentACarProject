using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Dto.RegisterDtos;
using System.Text;

namespace RentACarProject.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateAppUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateRegisterDto createRegisterDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createRegisterDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7262/api/Registers/CreateUser", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAppMemberUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppMemberUser(CreateMemberRegisterDto createMemberRegisterDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMemberRegisterDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7262/api/Registers/CreateMember", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SignUp");
            }
            return View();
        }
    }
}
