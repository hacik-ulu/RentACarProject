using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.AboutDtos
{
    public class CreateAboutDto
    {
        [Required(ErrorMessage = ("Title is required."))]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Name must be between 10 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = ("Description is required."))]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 100 and 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = ("Url is required."))]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string ImageUrl { get; set; }
    }
}
