using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Features.Mediator.Queries.AppUserQueries;
using RentACarProject.Application.Tools;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
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

        [HttpGet("GetAdminDetailsById")]
        public async Task<IActionResult> GetAdminDetailsById(int id)
        {
            var query = new GetAdminAccountDetailsByIDQuery { AppUserID = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("UpdateAdminUsername")]
        public async Task<IActionResult> UpdateAdminUsername(UpdateAdminUsernameCommand command)
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

        [HttpPut("UpdateAdminEmail")]
        public async Task<IActionResult> UpdateAdminEmail(UpdateAdminEmailCommand command)
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
