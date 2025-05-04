using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeysomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Saved_AccountId",
                table: "Saved");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_AccountId",
                table: "Saved",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Saved_AccountId",
                table: "Saved");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_AccountId",
                table: "Saved",
                column: "AccountId",
                unique: true);
        }
    }
}
