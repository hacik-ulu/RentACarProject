using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands
{
    public class CreateBrandCommand
    {
        [BindProperty]
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        [CustomBrandExist(ErrorMessage = "Brand name already exists.")]
        public string Name { get; set; }

        public bool IsExist(string brandName)
        {
            // Connection string'i buraya ekleyin
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

    public class CustomBrandExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var command = (CreateBrandCommand)validationContext.ObjectInstance;

            // Markanın veritabanında olup olmadığını kontrol ediyoruz
            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
