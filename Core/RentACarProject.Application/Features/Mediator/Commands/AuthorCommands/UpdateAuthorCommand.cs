using MediatR;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.Features.Mediator.Commands.AuthorCommands;
using RentACarProject.Application.ValidationAttributes.AuthorAttributes.UpdateAuthorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.AuthorCommands
{
    public class UpdateAuthorCommand :IRequest
    {
        [Required(ErrorMessage = "AuthorID is required.")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [CustomAuthorExist]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "Invalid URL format. Please enter a valid video URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; } // Description property was missing a type definition
        public string NormalizeAuthorName(string authorName)
        {
            return authorName.Replace('ı', 'i')
                            .Replace('ç', 'c')
                            .Replace('ş', 's')
                            .Replace('ğ', 'g')
                            .Replace('ü', 'u')
                            .Replace('ö', 'o');
        }
        public bool IsExist(string featureName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Authors WHERE Name = @AuthorName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Girdiyi normalize ediyoruz: boşlukları kaldır ve küçük harfe çevir
                    //string normalizedFeatureName = NormalizeFeatureName(featureName.Trim().Replace(" ", "").ToLowerInvariant());

                    string normalizedAuthorName = NormalizeAuthorName(featureName.Trim().ToLowerInvariant());
                    command.Parameters.AddWithValue("@AuthorName", normalizedAuthorName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}

//TURKCE KARAKTERLERİ ALSIN