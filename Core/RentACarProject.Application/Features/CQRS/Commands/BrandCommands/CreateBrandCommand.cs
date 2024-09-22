using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

public class CreateBrandCommand
{
    [Required(ErrorMessage = "Brand name is required.")]
    [MinLength(2, ErrorMessage = "Brand name must be at least 2 characters long.")]
    [MaxLength(25, ErrorMessage = "Brand name must be maximum 25 characters long.")]
    public string Name { get; set; }
}
