using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderItems_SubCategories_SubCategoryId",
                table: "SaleOrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderItems_SubCategories_SubCategoryId",
                table: "SaleOrderItems",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderItems_SubCategories_SubCategoryId",
                table: "SaleOrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderItems_SubCategories_SubCategoryId",
                table: "SaleOrderItems",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }
    }
}
