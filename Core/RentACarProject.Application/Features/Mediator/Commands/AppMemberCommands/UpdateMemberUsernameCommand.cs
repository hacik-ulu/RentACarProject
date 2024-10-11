using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands
{
    public class UpdateMemberUsernameCommand : IRequest
    {
        public int AppUserID { get; set; }
        public string NewUsername { get; set; }
    }
}
