using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Features.Mediator.Commands.FeatureCommands;
using RentACarProject.Application.Features.Mediator.Queries.CarPricingQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPricingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarPricingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CarPricingsWithCarList()
        {
            var values = await _mediator.Send(new GetCarPricingWithCarQuery());
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _mediator.Send(command);
                return Ok("Car Pricing Added!");
            }

        }

        [HttpGet("GetCarPricingWithTimePeriodList")]
        public async Task<IActionResult> GetCarPricingWithTimePeriodList()
        {
            var values = await _mediator.Send(new GetCarPricingWithTimePeriodQuery());
            return Ok(values);
        }
    }
}
