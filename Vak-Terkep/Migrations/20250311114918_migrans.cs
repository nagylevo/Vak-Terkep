using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vak_Terkep.Migrations
{
    /// <inheritdoc />
    public partial class migrans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaverId",
                table: "Saved");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaverId",
                table: "Saved",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
