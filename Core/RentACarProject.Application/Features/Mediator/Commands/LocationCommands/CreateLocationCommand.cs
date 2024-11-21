using MediatR;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.ValidationAttributes.LocationAttributes.CreateLocationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.LocationCommands
{
    public class CreateLocationCommand : IRequest
    {
        [Required(ErrorMessage = "Location Name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        [CustomLocationExist(ErrorMessage = "Location name already exists.")]
        public string Name { get; set; }

        public string NormalizeFeatureName(string locationName)
        {
            return locationName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }

        public bool IsExist(string locationName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Locations WHERE Name COLLATE Latin1_General_CI_AI = @LocationName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedLocationName = NormalizeFeatureName(locationName.Trim());
                    command.Parameters.AddWithValue("@LocationName", normalizedLocationName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
