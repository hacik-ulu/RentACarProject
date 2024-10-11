using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands
{
    public class UpdateMemberUsernameCommand : IRequest
    {
        [Required(ErrorMessage ="Member is required.")]
        public int AppUserID { get; set; }

        [StringLength(30, ErrorMessage = "Username must be between 6 and 30 characters long.", MinimumLength = 6)]
        public string NewUsername { get; set; }
    }
}
