using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

public class CreateBrandCommand
{
    [Required(ErrorMessage = "Brand name is required.")]
    //[MinLength(2, ErrorMessage = "Brand name must be at least 2 characters long.")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
    public string Name { get; set; }
}
