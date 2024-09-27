using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.ValidationAttributes.FeatureAttributes.CreateFeatureAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentACarProject.Application.Features.Mediator.Commands.FeatureCommands
{
    public class CreateFeatureCommand : IRequest
    {
        [BindProperty]
        [Required(ErrorMessage = ("Name is required."))]
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
                    //string normalizedFeatureName = NormalizeFeatureName(featureName.Trim().Replace(" ", "").ToLowerInvariant());

                    string normalizedFeatureName = NormalizeFeatureName(featureName.Trim().ToLowerInvariant());
                    command.Parameters.AddWithValue("@FeatureName", normalizedFeatureName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
       


    }
}