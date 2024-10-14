using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.CarPricingDtos;
using RentACarProject.Dto.LoginDtos;
using RentACarProject.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentACarProject.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7262/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = System.Text.Json.JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("accessToken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                        // Kullanıcı rolüne göre yönlendirme
                        if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                        {
                            return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
                        }
                        else if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
                        {
                            return RedirectToAction("Index", "Default");
                        }
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(changePasswordDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7262/api/Login/ChangePassword", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAdminDetailsById(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var appUserId = int.Parse(userIdClaim.Value);
            ViewBag.id = appUserId;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/Login/GetAdminDetailsById?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultAdminDetailsByIDDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateAdminUsername(int id)
        {
            var model = new UpdateAdminUsernameDto { AppUserID = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdminUsername(UpdateAdminUsernameDto updateModel)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7262/api/Login/UpdateAdminUsername", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAdminDetailsById", "Login", new { id = updateModel.AppUserID });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update username.");
                    return View(updateModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(updateModel);
            }
        }

        [HttpGet]
        public IActionResult UpdateAdminEmail(int id)
        {
            var model = new UpdateAdminEmailDto { AppUserID = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdminEmail(UpdateAdminEmailDto updateModel)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7262/api/Login/UpdateAdminEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAdminDetailsById", "Login", new { id = updateModel.AppUserID });
                }
                else
                {
                    // Read error response details
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    // Log or handle the error response
                    ModelState.AddModelError(string.Empty, $"Failed to update email. Server response: {errorResponse}");
                    return View(updateModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(updateModel);
            }
        }






    }
}