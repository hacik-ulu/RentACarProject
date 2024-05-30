using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Queries.CarDescriptionQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDescriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarDescriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarDescriptionsByCarID(int id)
        {
            var values = await _mediator.Send(new GetCarDescriptionByCarIDQuery(id));
            return Ok(values);
        }
    }
}
