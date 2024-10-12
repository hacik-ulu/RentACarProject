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
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(createSignUpDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7262/api/MemberLogin", content);

            bool hasError = false; 

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

                        if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Member"))
                        {
                            return RedirectToAction("Index", "Default");
                        }
                        else
                        {
                            TempData["Message"] = "There is no account. Please register.";
                            hasError = true; 
                        }
                    }
                }
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["Message"] = $"Error: {errorMessage}";
                hasError = true; 
            }

         
            if (hasError)
            {
                return View(createSignUpDto); 
            }

            return View(); 
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

