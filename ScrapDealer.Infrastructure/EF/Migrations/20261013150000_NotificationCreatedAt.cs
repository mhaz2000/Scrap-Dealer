using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ScrapDealer.Infrastructure.EF.Contexts;

#nullable disable

namespace ScrapDealer.Infrastructure.Migrations.ReadDb
{
    [DbContext(typeof(ReadDbContext))]
    [Migration("20261013150000_NotificationCreatedAt")]
    public partial class NotificationCreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");
        }
    }
}
