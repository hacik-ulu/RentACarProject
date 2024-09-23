using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

public class CreateBrandCommand
{
    [BindProperty]
    [Required(ErrorMessage = "Brand name is required.")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
    public string Name { get; set; }
}
