﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.LocationCommands;
using RentACarProject.Application.Features.Mediator.Queries.LocationQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocationList()
        {
            var values = await _mediator.Send(new GetLocationQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var value = await _mediator.Send(new GetLocationByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _mediator.Send(command);
                return Ok("Location Added!");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            await _mediator.Send(new RemoveLocationCommand(id));
            return Ok("Location Deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(UpdateLocationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _mediator.Send(command);
                return Ok("Location Updated!");
            }

        }
    }
}
