using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class addpricerange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SubCategories",
                newName: "MinPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "SubCategories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "MinPrice",
                table: "SubCategories",
                newName: "Price");
        }
    }
}
