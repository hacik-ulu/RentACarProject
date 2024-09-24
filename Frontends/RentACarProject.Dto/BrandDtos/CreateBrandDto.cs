using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.BrandAttributes.CreateBrandAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentACarProject.Dto.BrandDtos
{
    public class CreateBrandDto
    {
        [BindProperty]
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        [CustomBrandExist(ErrorMessage = "Brand name already exists.")]
        [IsOnlyLetters(ErrorMessage = "Only letters can be used.")] // Custom validation attribute

        public string Name { get; set; }

        public string NormalizeBrandName(string brandName)
        {
            return brandName.Replace('ı', 'i')
                            .Replace('ç', 'c')
                            .Replace('ş', 's')
                            .Replace('ğ', 'g')
                            .Replace('ü', 'u')
                            .Replace('ö', 'o');
        }

        public bool IsExist(string brandName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Brands WHERE Name = @BrandName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Girdiyi normalize ediyoruz
                    string normalizedBrandName = NormalizeBrandName(brandName.ToLowerInvariant());
                    command.Parameters.AddWithValue("@BrandName", normalizedBrandName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Custom validation attribute for checking only letters
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
