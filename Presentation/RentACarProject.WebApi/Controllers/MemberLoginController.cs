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
        public async Task<IActionResult> Index(GetCheckAppMemberQuery query)
        {
            var values = await _mediator.Send(query);
            if (values.IsExist)
            {
                return Created("", MemberJwtTokenGenerator.GenerateToken(values));
            }
            else
            {
                return BadRequest("Email or Password is false!");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeMemberPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password updated sucessfully!");
        }


        [HttpGet("GetMemberDetailsById")]
        public async Task<IActionResult> GetMemberDetailsById(int id)
        {
            var query = new GetMemberAccountDetailsByIdQuery { AppUserID = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpPut("UpdateMemberUsername")]
        public async Task<IActionResult> UpdateMemberUsername(UpdateMemberUsernameCommand command)
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

        [HttpPut("UpdateMemberEmail")]
        public async Task<IActionResult> UpdateMemberEmail(UpdateMemberEmailCommand command)
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


