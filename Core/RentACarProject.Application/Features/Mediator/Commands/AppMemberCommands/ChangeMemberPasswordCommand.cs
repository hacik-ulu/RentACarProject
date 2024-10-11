using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands
{
    public class ChangeMemberPasswordCommand : IRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
