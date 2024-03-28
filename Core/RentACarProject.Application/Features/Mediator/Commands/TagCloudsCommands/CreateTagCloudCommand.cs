using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.TagCloudsCommands
{
    public class CreateTagCloudCommand :IRequest
    {
        public string Name { get; set; }
        public int BlogID { get; set; }
    }
}
