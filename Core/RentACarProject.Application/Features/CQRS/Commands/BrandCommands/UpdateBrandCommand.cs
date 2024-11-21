using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentACarProject.Application.ValidationAttributes.BrandAttributes;
using RentACarProject.Application.ValidationAttributes.BrandAttributes.UpdateBrandAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

public class UpdateBrandCommand
{
    [Required(ErrorMessage = ("Id is required."))]
    public int BrandID { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Brand name is required.")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
    [CustomBrandExist(ErrorMessage = "Brand name already exists.")]
    public string Name { get; set; }
    public string NormalizeBrandName(string brandName)
    {
        return brandName.Trim(); // Yalnızca baştaki ve sondaki boşlukları sil, başka bir işleme gerek yok
    }

    public bool IsExist(string brandName)
    {
        string connectionString = "Server=HACIKULU\\SQLEXPRESS;Initial Catalog=RentACarDb;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            // COLLATE ile büyük/küçük harf ve Türkçe karakter duyarlılığını kaldırıyoruz
            string query = "SELECT COUNT(1) FROM Brands WHERE Name COLLATE Latin1_General_CI_AI = @BrandName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                string normalizedBrandName = NormalizeBrandName(brandName.Trim());
                command.Parameters.AddWithValue("@BrandName", normalizedBrandName);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }

}
