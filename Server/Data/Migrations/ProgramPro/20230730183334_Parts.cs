using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramPro.Server.Data.Migrations.ProgramPro
{
    /// <inheritdoc />
    public partial class Parts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Weeks_WeekId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Days",
                newName: "PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_WeekId",
                table: "Days",
                newName: "IX_Days_PartId");

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingprogramId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountOfDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Trainingprograms_TrainingprogramId",
                        column: x => x.TrainingprogramId,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_TrainingprogramId",
                table: "Parts",
                column: "TrainingprogramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Parts_PartId",
                table: "Days",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Parts_PartId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "Days",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_PartId",
                table: "Days",
                newName: "IX_Days_WeekId");

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_Trainingprograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_ProgramId",
                table: "Weeks",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Weeks_WeekId",
                table: "Days",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
