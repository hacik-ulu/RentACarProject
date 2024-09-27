﻿using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.AuthorAttributes.CreateAuthorAttributes;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes.CreateFeatureAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.AuthorDtos
{
    public class CreateAuthorDto
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
        public string Description { get; set; } // Description property was missing a type definition

        public string NormalizeFeatureName(string authorName)
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

                    string normalizedFeatureName = NormalizeFeatureName(featureName.Trim().ToLowerInvariant());
                    command.Parameters.AddWithValue("@AuthorName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
