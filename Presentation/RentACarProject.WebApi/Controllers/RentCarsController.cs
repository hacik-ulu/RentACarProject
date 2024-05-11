using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Queries.RentCarQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentCarsController : ControllerBase
    {
        private readonly IMediator _meditor;

        public RentCarsController(IMediator meditor)
        {
            _meditor = meditor;
        }

        [HttpPost]
        public async Task<IActionResult> GetRentCarByLocationList(GetRentCarQuery query)
        {
            var values = await _meditor.Send(query);
            return Ok(values);
        }
    }
}
