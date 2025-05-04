using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeytryagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Accounts_AccountId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_AccountId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Routes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_AccountId",
                table: "Routes",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Accounts_AccountId",
                table: "Routes",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
