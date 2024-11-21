using MediatR;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.ValidationAttributes.AuthorAttributes.CreateAuthorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AuthorCommands
{
    public class CreateAuthorCommand : IRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [CustomAuthorExist(ErrorMessage = "Author name already exists.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; } 

        public string NormalizeFeatureName(string authorName)
        {
            return authorName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }

        public bool IsExist(string featureName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Authors WHERE Name COLLATE Latin1_General_CI_AI = @AuthorName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedFeatureName = NormalizeFeatureName(featureName.Trim());
                    command.Parameters.AddWithValue("@AuthorName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
