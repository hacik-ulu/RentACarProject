using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarDtos
{
    public class CreateCarDto
    {
        [BindProperty]
        [Required(ErrorMessage = "Brand is required.")]
        public int BrandID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Model must be between 1 and 50 characters long.")]
        public string Model { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Cover image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string CoverImagerUrl { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Mileage is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Mileage must be a positive value.")]
        public decimal Mileage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Year is required.")]
        [Range(2017, int.MaxValue, ErrorMessage = "Year must be greater than 2016.")]
        public int Year { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Transmission type is required.")]
        [StringLength(20, ErrorMessage = "Transmission type must be less than 20 characters.")]
        public string Transmission { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Seats are required.")]
        [Range(1, 10, ErrorMessage = "Seats must be between 1 and 10.")]
        public byte Seat { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Luggage capacity is required.")]
        [Range(0, 10, ErrorMessage = "Luggage capacity must be between 0 and 10.")]
        public byte Luggage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Fuel type is required.")]
        [StringLength(20, ErrorMessage = "Fuel type must be less than 20 characters.")]
        public string Fuel { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Big image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string BigImageUrl { get; set; }

    }
}
