using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramPro.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Component : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayDefinitions_SplitDefinitions_SplitDefinitionId",
                table: "DayDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_Splits_SplitId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "SplitDefinitions");

            migrationBuilder.DropTable(
                name: "Splits");

            migrationBuilder.RenameColumn(
                name: "SplitId",
                table: "Days",
                newName: "ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_SplitId",
                table: "Days",
                newName: "IX_Days_ComponentId");

            migrationBuilder.RenameColumn(
                name: "SplitDefinitionId",
                table: "DayDefinitions",
                newName: "ComponentDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_DayDefinitions_SplitDefinitionId",
                table: "DayDefinitions",
                newName: "IX_DayDefinitions_ComponentDefinitionId");

            migrationBuilder.CreateTable(
                name: "ComponentDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaysInASplit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentDefinitions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDefinitions_ApplicationUserId",
                table: "ComponentDefinitions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_TrainingProgramId",
                table: "Components",
                column: "TrainingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayDefinitions_ComponentDefinitions_ComponentDefinitionId",
                table: "DayDefinitions",
                column: "ComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Components_ComponentId",
                table: "Days",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayDefinitions_ComponentDefinitions_ComponentDefinitionId",
                table: "DayDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_Components_ComponentId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "ComponentDefinitions");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.RenameColumn(
                name: "ComponentId",
                table: "Days",
                newName: "SplitId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_ComponentId",
                table: "Days",
                newName: "IX_Days_SplitId");

            migrationBuilder.RenameColumn(
                name: "ComponentDefinitionId",
                table: "DayDefinitions",
                newName: "SplitDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_DayDefinitions_ComponentDefinitionId",
                table: "DayDefinitions",
                newName: "IX_DayDefinitions_SplitDefinitionId");

            migrationBuilder.CreateTable(
                name: "SplitDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DaysInASplit = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplitDefinitions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Splits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SplitNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Splits_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SplitDefinitions_ApplicationUserId",
                table: "SplitDefinitions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Splits_TrainingProgramId",
                table: "Splits",
                column: "TrainingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayDefinitions_SplitDefinitions_SplitDefinitionId",
                table: "DayDefinitions",
                column: "SplitDefinitionId",
                principalTable: "SplitDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Splits_SplitId",
                table: "Days",
                column: "SplitId",
                principalTable: "Splits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
