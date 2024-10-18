using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Dto.ReservationDtos;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace RentACarProject.WebUI.Controllers
{
    public class InformationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public InformationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Rentals(int id)
        {
            ViewBag.v1 = "RENTALS ";
            ViewBag.v2 = "My Rental Informations ";

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Rentals", "Information", new { id });
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7262/api/Reservations/GetReservationByUserId/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // Veriyi deserialize etme
                var values = JsonConvert.DeserializeObject<List<GetReservationByUserIdDto>>(jsonData);

                // Eğer gelen veri tek bir nesne ise onu listeye çeviriyoruz
                if (values == null || !values.Any())
                {
                    // Eğer tek bir nesne gelmişse, onu liste olarak al
                    var singleValue = JsonConvert.DeserializeObject<GetReservationByUserIdDto>(jsonData);
                    if (singleValue != null)
                    {
                        values = new List<GetReservationByUserIdDto> { singleValue };
                    }
                }

                // Veriyi view'a gönder
                return View(values);
            }

            // API çağrısı başarısız olduysa uygun bir hata mesajı verebiliriz
            ViewBag.ErrorMessage = "Failed to retrieve reservations.";
            return View(new List<GetReservationByUserIdDto>()); // Boş bir liste döndürüyoruz
        }

    }
}
