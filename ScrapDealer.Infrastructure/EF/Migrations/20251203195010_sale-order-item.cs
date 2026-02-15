using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class saleorderitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderItems_SaleOrders_SaleOrderReadModelId",
                table: "SaleOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderItems_SaleOrderReadModelId",
                table: "SaleOrderItems");

            migrationBuilder.DropColumn(
                name: "SaleOrderReadModelId",
                table: "SaleOrderItems");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleOrderId",
                table: "SaleOrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_SaleOrderId",
                table: "SaleOrderItems",
                column: "SaleOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderItems_SaleOrders_SaleOrderId",
                table: "SaleOrderItems",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderItems_SaleOrders_SaleOrderId",
                table: "SaleOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderItems_SaleOrderId",
                table: "SaleOrderItems");

            migrationBuilder.DropColumn(
                name: "SaleOrderId",
                table: "SaleOrderItems");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleOrderReadModelId",
                table: "SaleOrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_SaleOrderReadModelId",
                table: "SaleOrderItems",
                column: "SaleOrderReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderItems_SaleOrders_SaleOrderReadModelId",
                table: "SaleOrderItems",
                column: "SaleOrderReadModelId",
                principalTable: "SaleOrders",
                principalColumn: "Id");
        }
    }
}
