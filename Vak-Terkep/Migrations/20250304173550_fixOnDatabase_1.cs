using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class fixOnDatabase_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Accounts_AccountsId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "AccountsId",
                table: "Routes",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_AccountsId",
                table: "Routes",
                newName: "IX_Routes_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Accounts_AccountId",
                table: "Routes",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Accounts_AccountId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Routes",
                newName: "AccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_AccountId",
                table: "Routes",
                newName: "IX_Routes_AccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Accounts_AccountsId",
                table: "Routes",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
