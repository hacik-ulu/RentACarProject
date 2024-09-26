using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.BannerDtos
{
    public class UpdateBannerDto
    {
        [Required(ErrorMessage = "Banner ID is required.")]
        public int BannerID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Video Description cannot be longer than 500 characters.")]
        [Required(ErrorMessage = "Video Description is required.")]
        public string VideoDescription { get; set; }

        [Required(ErrorMessage = "Video URL is required.")]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string VideoUrl { get; set; }
    }
}
