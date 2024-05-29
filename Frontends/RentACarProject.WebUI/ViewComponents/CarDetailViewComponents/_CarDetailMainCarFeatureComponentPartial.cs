using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarProject.Dto.CarDtos;
using RentACarProject.Dto.CategoryDtos;
using System.Net.Http;

namespace RentACarProject.WebUI.ViewComponents.CarDetailViewComponents
{
	public class _CarDetailMainCarFeatureComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _CarDetailMainCarFeatureComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			ViewBag.CarID = id;
			var client = _httpClientFactory.CreateClient();
			var resposenMessage = await client.GetAsync($"https://localhost:7262/api/Cars/{id}");
			if (resposenMessage.IsSuccessStatusCode)
			{
				var jsonData = await resposenMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultCarWithBrandsDto>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
