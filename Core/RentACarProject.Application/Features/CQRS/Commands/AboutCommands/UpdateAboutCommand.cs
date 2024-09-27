﻿using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.CQRS.Commands.AboutCommands;

public class UpdateAboutCommand
{
    [Required(ErrorMessage = ("AboutId is required."))]
    public int AboutId { get; set; }

    [Required(ErrorMessage = ("Title is required."))]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Name must be between 10 and 100 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = ("Description is required."))]
    [StringLength(130, MinimumLength = 10, ErrorMessage = "Description must be between 100 and 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = ("Url is required."))]
    [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
    public string ImageUrl { get; set; }
}
