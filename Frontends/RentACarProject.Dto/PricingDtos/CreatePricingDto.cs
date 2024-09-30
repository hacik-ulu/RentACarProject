using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.PricingTypesAttributes.CreatePricingTypeAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.PricingDtos
{
    public class CreatePricingDto
    {
        [Required(ErrorMessage = "Pricing Type is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        [CustomPricingTypeExist(ErrorMessage = "Pricing type already exists.")]
        public string Name { get; set; }

        public string NormalizeFeatureName(string pricingName)
        {
            return pricingName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }

        public bool IsExist(string pricingName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Pricings WHERE Name COLLATE Latin1_General_CI_AI = @PricingName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedPricingName = NormalizeFeatureName(pricingName.Trim());
                    command.Parameters.AddWithValue("@PricingName", normalizedPricingName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
