using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.ReservationCommands;
using RentACarProject.Application.Features.Mediator.Queries.ReservationQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetReservationList")]
        public async Task<IActionResult> GetReservationList()
        {
            var values = await _mediator.Send(new GetReservationQuery());
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
        {
            await _mediator.Send(command);
            return Ok("Reservation added!");
        }
    }
}
