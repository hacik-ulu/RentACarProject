using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Features.Mediator.Queries.AppMembersQueries;
using RentACarProject.Application.Features.Mediator.Queries.AppUserQueries;
using RentACarProject.Application.Tools;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberLoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MemberLoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetCheckAppUserQuery query)
        {
            var values = await _mediator.Send(query);
            if (values.IsExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(values));
            }
            else
            {
                return BadRequest("Username or Password is false!");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password updated sucessfully!");
        }

        [HttpGet("GetMemberDetailsById")]
        public async Task<IActionResult> GetUserDetailsById(int id)
        {
            var query = new GetMemberAccountDetailsByIdQuery { AppUserID = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("UpdateUserUsername")]
        public async Task<IActionResult> UpdateUserUsername(UpdateMemberUsernameCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Username updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail(UpdateMemberEmailCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Email updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
