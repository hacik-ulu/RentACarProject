using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.BrandAttributes.CreateBrandAttributes;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.BrandDtos
{
    public class CreateBrandDto
    {
        [BindProperty]
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters allowed!")]
        [CustomBrandExist(ErrorMessage = "Brand name already exists.")]

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

    }
}
