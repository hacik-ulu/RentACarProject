using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Dto.LoginDtos;
using RentACarProject.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using RentACarProject.Dto.SignUpDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace RentACarProject.WebUI.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SignUpController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateSignUpDto createSignUpDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createSignUpDto); 
            }

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(createSignUpDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7262/api/MemberLogin", content);

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

                    var (name, surname) = await GetUserFullNameByEmailAsync(createSignUpDto.Email);
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname))
                    {
                        claims.Add(new Claim("Name", name));
                        claims.Add(new Claim("Surname", surname));
                    }

                    claims.Add(new Claim("accessToken", tokenModel.Token));
                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties
                    {
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                    if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
                    {
                        return RedirectToAction("Index", "Default");
                    }
                    else
                    {
                        TempData["Message"] = "There is no account. Please register.";
                    }
                }
            }
            else
            {
                // Hata mesajlarını kontrol et
                var errorMessage = await response.Content.ReadAsStringAsync();

                if (errorMessage.Contains("You need to register"))
                {
                    ModelState.AddModelError(string.Empty, "You need to register."); 
                }
                else if (errorMessage.Contains("Password is incorrect"))
                {
                    ModelState.AddModelError(string.Empty, "Password is incorrect."); 
                }
                else
                {
                    TempData["Message"] = $"Hata: {errorMessage}"; 
                }
            }

            return View(createSignUpDto);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeMemberPasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(changePasswordDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7262/api/MemberLogin/ChangePassword", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SignUp");
            }

            var errorMessage = await responseMessage.Content.ReadAsStringAsync();

            TempData["ErrorMessage"] = errorMessage;

            return View(changePasswordDto); 
        }








        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }

        private async Task<(string Name, string Surname)> GetUserFullNameByEmailAsync(string email)
        {
            using (var connection = new SqlConnection("Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;"))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT Name, Surname FROM [AppUsers] WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var name = reader["Name"].ToString();
                        var surname = reader["Surname"].ToString();
                        return (name, surname);
                    }
                }
            }

            return (null, null); 
        }
    }
}

// Register sayfası yapılacak.