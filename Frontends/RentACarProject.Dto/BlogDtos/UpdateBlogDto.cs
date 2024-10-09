using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.BlogDtos
{
    public class UpdateBlogDto
    {
        [Required(ErrorMessage ="BlogID is required.")]
        public int BlogID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 100 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public int? AuthorID { get; set; }

        [Required(ErrorMessage = "Cover Image Url is required.")]
        [Url(ErrorMessage = "Invalid Cover Image URL.")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1200, MinimumLength = 100, ErrorMessage = "Description must be between 10 and 1200 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int? CategoryID { get; set; }
    }
}
