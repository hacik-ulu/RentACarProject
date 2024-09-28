using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.ServiceAttributes.UpdateServiceAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.ServiceDtos
{
    public class UpdateServiceDto
    {
        [Required(ErrorMessage = "ServiceId is required.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        [CustomServiceExist(ErrorMessage = "Service name already exists.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image Url is required.Please enter valid type Image Url.")]
        public string ImageUrl { get; set; }
        public string NormalizeServiceName(string serviceName)
        {
            return serviceName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }
        public bool IsExist(string serviceName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Services WHERE Title COLLATE Latin1_General_CI_AI = @ServiceName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedServiceName = NormalizeServiceName(serviceName.Trim());
                    command.Parameters.AddWithValue("@ServiceName", normalizedServiceName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
