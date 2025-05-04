using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saver",
                table: "Saved",
                newName: "buildingName");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Saved",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SaverId",
                table: "Saved",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Saved");

            migrationBuilder.DropColumn(
                name: "SaverId",
                table: "Saved");

            migrationBuilder.RenameColumn(
                name: "buildingName",
                table: "Saved",
                newName: "Saver");
        }
    }
}
