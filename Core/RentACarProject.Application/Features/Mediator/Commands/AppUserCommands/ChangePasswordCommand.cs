using MediatR;
using RentACarProject.Application.Features.Mediator.Results.AppUserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AppUserCommands
{
    public class ChangePasswordCommand : IRequest
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
