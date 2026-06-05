using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class lastupdateadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "SubCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Categories");
        }
    }
}
