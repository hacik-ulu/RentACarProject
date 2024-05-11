using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_RentCarTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpLocationID",
                table: "RentCars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickUpLocationID",
                table: "RentCars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
