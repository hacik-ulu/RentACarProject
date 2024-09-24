using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.FeatureDtos
{
    public class CreateFeatureDto
    {
        [Required(ErrorMessage = ("Feature Name is required."))]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters allowed!")]
        [CustomFeatureExist(ErrorMessage = "Feature name already exists.")]
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
        public bool IsExist(string featureName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Features WHERE Name = @FeatureName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Girdiyi normalize ediyoruz
                    string normalizedFeatureName = NormalizeBrandName(featureName.ToLowerInvariant());
                    command.Parameters.AddWithValue("@FeatureName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
