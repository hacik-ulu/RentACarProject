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

        [HttpGet]
        public async Task<IActionResult> GetRentCarByLocationList(int locationID, bool available)
        {
            GetRentCarQuery query = new GetRentCarQuery()
            {
                Available = available,
                LocationID = locationID
            };
            var values = await _meditor.Send(query);
            return Ok(values);
        }
    }
}
