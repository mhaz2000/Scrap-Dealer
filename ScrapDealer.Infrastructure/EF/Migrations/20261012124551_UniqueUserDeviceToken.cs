using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.Migrations.ReadDb
{
    /// <inheritdoc />
    public partial class UniqueUserDeviceToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                WITH RankedDeviceTokens AS
                (
                    SELECT Id,
                           ROW_NUMBER() OVER (
                               PARTITION BY UserId, Token
                               ORDER BY IsActive DESC, UpdatedAt DESC, CreatedAt DESC, Id DESC
                           ) AS RowNumber
                    FROM UserDeviceTokens
                )
                DELETE FROM RankedDeviceTokens
                WHERE RowNumber > 1;
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_UserDeviceTokens_UserId_Token'
                      AND object_id = OBJECT_ID(N'UserDeviceTokens')
                )
                    DROP INDEX IX_UserDeviceTokens_UserId_Token ON UserDeviceTokens;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeviceTokens_UserId_Token",
                table: "UserDeviceTokens",
                columns: new[] { "UserId", "Token" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP INDEX IF EXISTS IX_UserDeviceTokens_UserId_Token ON UserDeviceTokens;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeviceTokens_UserId_Token",
                table: "UserDeviceTokens",
                columns: new[] { "UserId", "Token" });
        }
    }
}
