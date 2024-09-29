using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.TestimonialCommands
{
    public class UpdateTestimonialCommand : IRequest
    {
        [Required(ErrorMessage = "TestimonialID is required.")]
        public int TestimonialID { get; set; }

        [Required(ErrorMessage = "Testimonial name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Comment must be between 5 and 150 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Video URL is required.")]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string ImageUrl { get; set; }
    }
}
