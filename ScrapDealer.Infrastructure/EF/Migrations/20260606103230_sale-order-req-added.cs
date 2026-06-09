using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class saleorderreqadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleOrderRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderRequests_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrderRequests_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderRequests_BuyerId",
                table: "SaleOrderRequests",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderRequests_SaleOrderId",
                table: "SaleOrderRequests",
                column: "SaleOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleOrderRequests");
        }
    }
}
