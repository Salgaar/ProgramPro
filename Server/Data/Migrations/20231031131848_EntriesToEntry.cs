using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramPro.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntriesToEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Entries_SetId",
                table: "Entries");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_SetId",
                table: "Entries",
                column: "SetId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Entries_SetId",
                table: "Entries");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_SetId",
                table: "Entries",
                column: "SetId");
        }
    }
}
