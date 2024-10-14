using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Application.Features.Mediator.Queries.AppMembersQueries;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Tools;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.LoginDtos;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberLoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<AppUser> _userRepository;
        public MemberLoginController(IMediator mediator, IRepository<AppUser> userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetCheckAppMemberQuery query)
        {
            var values = await _mediator.Send(query);

            if (!values.IsExist)
            {
                return BadRequest("You need to register."); 
            }

            // E-posta mevcut ama şifre hatalıysa
            var user = await _userRepository.GetByFilterAsync(x => x.Email == query.Email);
            if (user != null && !user.Password.Equals(query.Password))
            {
                return BadRequest("Password is incorrect."); 
            }

            return Created("", MemberJwtTokenGenerator.GenerateToken(values));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeMemberPasswordCommand command)
        {
            var existingUser = await _userRepository.GetByFilterAsync(x => x.Email == command.Email);

            if (existingUser == null)
            {
                return BadRequest("Please check your email address."); 
            }

            await _mediator.Send(command);
            return Ok("Password updated successfully!");
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


