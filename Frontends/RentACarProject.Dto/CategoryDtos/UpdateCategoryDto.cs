using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Dto.ValidationAttributes.CategoryAttributes.UpdateCategoryAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CategoryDtos
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 3 and 25 characters long.")]
        [CustomCategoryExist(ErrorMessage = "Category name ready exists.")]
        public string Name { get; set; }

        public string NormalizeCategoryName(string categoryName)
        {
            return categoryName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
        }
        public bool IsExist(string categoryName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
                string query = "SELECT COUNT(1) FROM Categories WHERE Name COLLATE Latin1_General_CI_AI = @CategoryName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    string normalizedCategoryName = NormalizeCategoryName(categoryName.Trim());
                    command.Parameters.AddWithValue("@CategoryName", normalizedCategoryName);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
