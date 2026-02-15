using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class ticketIdaddedtomessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Tickets_TicketReadModelId",
                table: "TicketMessages");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessages_TicketReadModelId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "TicketReadModelId",
                table: "TicketMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "TicketMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessages",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketReadModelId",
                table: "TicketMessages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_TicketReadModelId",
                table: "TicketMessages",
                column: "TicketReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Tickets_TicketReadModelId",
                table: "TicketMessages",
                column: "TicketReadModelId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
