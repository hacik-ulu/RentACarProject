using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes.UpdateFeatureAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentACarProject.Dto.FeatureDtos
{
    public class UpdateFeatureDto
    {
        [BindProperty]
        [Required(ErrorMessage = ("Id is required."))]
        public int FeatureID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = ("Name is required."))]
        [IsOnlyLetters(ErrorMessage = "Only letters can be used.")] // Custom validation attribute
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [CustomFeatureExist(ErrorMessage = "Feature name already exists.")]
        public string Name { get; set; }

        public string NormalizeFeatureName(string featureName)
        {
            return featureName.Replace('ı', 'i')
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
                string query = "SELECT COUNT(1) FROM Features WHERE Name = @FeatureName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Girdiyi normalize ediyoruz: boşlukları kaldır ve küçük harfe çevir
                    string normalizedFeatureName = NormalizeFeatureName(featureName.Trim().Replace(" ", "").ToLowerInvariant());
                    command.Parameters.AddWithValue("@FeatureName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public class IsOnlyLettersAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    string input = value.ToString();
                    // Yalnızca harflerin bulunduğunu kontrol eden regex
                    if (!Regex.IsMatch(input, @"^[A-Za-z]+$"))
                    {
                        return new ValidationResult(ErrorMessage ?? "Only letters are allowed.");
                    }
                }
                return ValidationResult.Success;
            }
        }


    }
}
