﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.WebUI.ValidationAttributes.BrandAttributes;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.BrandDtos
{
    public class CreateBrandDto
    {
        [BindProperty]
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        [CustomBrandExist(ErrorMessage = "Brand name already exists.")]

        public string Name { get; set; }

        public bool IsExist(string brandName)
        {
            string connectionString = "Server=HACIKULU\\SQLEXPRESS;initial Catalog=RentACarDb;integrated security=true;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Brands WHERE Name = @BrandName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BrandName", brandName);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
