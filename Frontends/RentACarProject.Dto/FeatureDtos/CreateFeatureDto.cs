using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes;
using RentACarProject.Dto.ValidationAttributes.FeatureAttributes.CreateFeatureAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentACarProject.Dto.FeatureDtos
{
    public class CreateFeatureDto
    {
        [BindProperty]
        [Required(ErrorMessage = ("Feature Name is required."))]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [CustomFeatureExist(ErrorMessage = "Feature name already exists.")]
        public string Name { get; set; }

        public string NormalizeFeatureName(string featureName)
        {
            return featureName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok

        }
        public bool IsExist(string featureName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Features WHERE Name COLLATE Latin1_General_CI_AI = @FeatureName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedFeatureName = NormalizeFeatureName(featureName.Trim());
                    command.Parameters.AddWithValue("@FeatureName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
