using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ScrapDealer.Infrastructure.EF.Contexts;

#nullable disable

namespace ScrapDealer.Infrastructure.Migrations.ReadDb
{
    [DbContext(typeof(ReadDbContext))]
    [Migration("20261106120000_InitialBuyerOwnsShop")]
    public partial class InitialBuyerOwnsShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OwnsShop",
                table: "InitialBuyers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnsShop",
                table: "InitialBuyers");
        }
    }
}
