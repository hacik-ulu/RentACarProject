using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Application.Features.Mediator.Commands.RegisterCommands;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User added sucessfully!");
        }

        [HttpPost("CreateMember")]
        public async Task<IActionResult> CreateMember(CreateAppMemberCommand command)
        {
            await _mediator.Send(command);
            return Ok("Member added sucessfully!");
        }
    }
}
