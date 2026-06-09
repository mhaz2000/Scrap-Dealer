using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class userfileadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NationalCardFileId",
                table: "Sellers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileFormFileId",
                table: "Sellers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessLicenseFileId",
                table: "Buyers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CarCardFileId",
                table: "Buyers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCardFileId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ProfileFormFileId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "CarCardFileId",
                table: "Buyers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessLicenseFileId",
                table: "Buyers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
