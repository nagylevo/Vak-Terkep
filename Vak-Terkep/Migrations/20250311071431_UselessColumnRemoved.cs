using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class UselessColumnRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arabicNumber",
                table: "Saved");

            migrationBuilder.DropColumn(
                name: "romanNumber",
                table: "Saved");

            migrationBuilder.AddColumn<string>(
                name: "Saver",
                table: "Saved",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saver",
                table: "Saved");

            migrationBuilder.AddColumn<int>(
                name: "arabicNumber",
                table: "Saved",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "romanNumber",
                table: "Saved",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
