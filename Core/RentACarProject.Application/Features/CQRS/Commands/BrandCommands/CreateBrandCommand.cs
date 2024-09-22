using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

public class CreateBrandCommand
{
    public string Name { get; set; }
}
