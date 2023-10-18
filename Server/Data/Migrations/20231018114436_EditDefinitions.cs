using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramPro.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysInASplit",
                table: "ComponentDefinitions");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "DayDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "DayDefinitions");

            migrationBuilder.AddColumn<int>(
                name: "DaysInASplit",
                table: "ComponentDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
