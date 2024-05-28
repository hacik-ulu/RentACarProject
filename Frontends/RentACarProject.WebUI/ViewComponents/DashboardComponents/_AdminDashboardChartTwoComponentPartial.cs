using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Dto.CarDtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentACarProject.WebUI.ViewComponents.DashboardComponents
{
    public class _AdminDashboardChartTwoComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AdminDashboardChartTwoComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var carResponseMessage = await client.GetAsync("https://localhost:7262/api/Cars/GetCarWithBrand");
            var brandResponseMessage = await client.GetAsync("https://localhost:7262/api/Brands");

            if (carResponseMessage.IsSuccessStatusCode && brandResponseMessage.IsSuccessStatusCode)
            {
                var carJsonData = await carResponseMessage.Content.ReadAsStringAsync();
                var brandJsonData = await brandResponseMessage.Content.ReadAsStringAsync();

                // Log response data for debugging
                System.Diagnostics.Debug.WriteLine("Car JSON Data: " + carJsonData);
                System.Diagnostics.Debug.WriteLine("Brand JSON Data: " + brandJsonData);

                var carValues = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(carJsonData);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandDto>>(brandJsonData);

                // Log deserialized data for debugging
                System.Diagnostics.Debug.WriteLine("Car Values: " + JsonConvert.SerializeObject(carValues));
                System.Diagnostics.Debug.WriteLine("Brand Values: " + JsonConvert.SerializeObject(brandValues));

                var viewModel = new Tuple<List<ResultCarWithBrandsDto>, List<ResultBrandDto>>(carValues, brandValues);

                return View(viewModel);
            }

            return View(new Tuple<List<ResultCarWithBrandsDto>, List<ResultBrandDto>>(new List<ResultCarWithBrandsDto>(), new List<ResultBrandDto>()));
        }
    }
}
