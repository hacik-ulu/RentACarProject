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
using Microsoft.AspNetCore.Authorization;
using RentACarProject.Domain.Entities;

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
        public IActionResult ChangeMemberPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMemberPassword(ChangeMemberPasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(changePasswordDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7262/api/MemberLogin/ChangeMemberPassword", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var appUserId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (appUserId != null)
                {
                    return RedirectToAction("GetMemberDetailsById", "SignUp", new { id = int.Parse(appUserId) });
                }

            }

            var errorMessage = await responseMessage.Content.ReadAsStringAsync();

            TempData["ErrorMessage"] = errorMessage;

            return View(changePasswordDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberDetailsById(int id)
        {
            ViewBag.v1 = "MY ACCOUNT ";
            ViewBag.v2 = "Member Details";

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "SignUp");
            }

            var appUserId = int.Parse(userIdClaim.Value);
            ViewBag.id = appUserId;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/MemberLogin/GetMemberDetailsById?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultMemberDetailsByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateMemberUsername(int id)
        {
            ViewBag.v1 = "CHANGE USERNAME ";
            ViewBag.v2 = "Change Username";

            var model = new UpdateMemberUsernameDto { AppUserID = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMemberUsername(UpdateMemberUsernameDto updateModel)
        {
            ViewBag.v1 = "CHANGE USERNAME ";
            ViewBag.v2 = "Change Username";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7262/api/MemberLogin/UpdateMemberUsername", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetMemberDetailsById", "SignUp", new { id = updateModel.AppUserID });
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
        public IActionResult UpdateMemberEmail(int id)
        {
            ViewBag.v1 = "CHANGE Email ";
            ViewBag.v2 = "Change Email";

            var model = new UpdateMemberEmailDto { AppUserID = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMemberEmail(UpdateMemberEmailDto updateModel)
        {
            ViewBag.v1 = "CHANGE Email ";
            ViewBag.v2 = "Change Email";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7262/api/MemberLogin/UpdateMemberEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetMemberDetailsById", "SignUp", new { id = updateModel.AppUserID });
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

// Kullanıcıya ait bir controller açılıp tek tek metodlar yapılar.MemberUI/UIMemberController(MVCde).

// Yapayn zeka ile kullanııı kendi sayfasından kendine en uygun araçı bulsun.