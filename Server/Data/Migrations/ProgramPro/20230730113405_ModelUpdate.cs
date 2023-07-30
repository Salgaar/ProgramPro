using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramPro.Server.Data.Migrations.ProgramPro
{
    /// <inheritdoc />
    public partial class ModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "PercentageOfOneRepMax",
                table: "PersonalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RIR",
                table: "PersonalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RPE",
                table: "PersonalRecords",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageOfOneRepMax",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RIR",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RPE",
                table: "Goals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Goals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ExerciseId",
                table: "Goals",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Exercises_ExerciseId",
                table: "Goals",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Exercises_ExerciseId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ExerciseId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "PercentageOfOneRepMax",
                table: "PersonalRecords");

            migrationBuilder.DropColumn(
                name: "RIR",
                table: "PersonalRecords");

            migrationBuilder.DropColumn(
                name: "RPE",
                table: "PersonalRecords");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "PercentageOfOneRepMax",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "RIR",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "RPE",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Goals");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Trainingprograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals",
                column: "ProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_ProgramId",
                table: "Results",
                column: "ProgramId",
                unique: true);
        }
    }
}
