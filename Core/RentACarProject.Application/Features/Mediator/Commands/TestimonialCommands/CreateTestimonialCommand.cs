using MediatR;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.ValidationAttributes.AuthorAttributes.CreateAuthorAttributes;
using RentACarProject.Application.ValidationAttributes.TestimonialAttributes.CreateTestimonialAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.TestimonialCommands
{
    public class CreateTestimonialCommand : IRequest
    {
        [Required(ErrorMessage = "Testimonial name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [CustomTestimonialExist(ErrorMessage = "Testimonial name already exists.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Comment must be between 5 and 150 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Video URL is required.")]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string ImageUrl { get; set; }
        public string NormalizeTestimonialName(string testimonialName)
        {
            return testimonialName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }
        public bool IsExist(string testimonialName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Testimonials WHERE Name COLLATE Latin1_General_CI_AI = @TestimonialName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedTestimonialName = NormalizeTestimonialName(testimonialName.Trim());
                    command.Parameters.AddWithValue("@TestimonialName", normalizedTestimonialName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
