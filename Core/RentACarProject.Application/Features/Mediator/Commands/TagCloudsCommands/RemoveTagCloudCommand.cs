using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.TagCloudsCommands
{
    public class RemoveTagCloudCommand :IRequest
    {
        public int Id { get; set; }

        public RemoveTagCloudCommand(int ıd)
        {
            Id = ıd;
        }
    }
}
