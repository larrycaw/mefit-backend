using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.Migrations
{
    public partial class SeedAndLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Workouts_WorkoutId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_WorkoutId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Sets");

            migrationBuilder.CreateTable(
                name: "WorkoutGoals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutGoals", x => new { x.GoalId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_WorkoutGoals_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkoutGoals_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPrograms",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPrograms", x => new { x.ProgramId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_WorkoutPrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkoutPrograms_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSets",
                columns: table => new
                {
                    SetId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSets", x => new { x.SetId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_WorkoutSets_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkoutSets_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "TargetMuscleGroup", "VideoURL" },
                values: new object[] { 1, "Biceps strong", "img", "Barbell curl", "biceps", ".mov" });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[] { 1, "Upper body", "Bicep enhancement" });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Complete", "Name", "Type" },
                values: new object[] { 1, false, "Arm day", "Strength" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "Achieved", "ProgramEndDate", "ProgramId" },
                values: new object[] { 1, false, new DateTime(2022, 3, 10, 14, 4, 22, 660, DateTimeKind.Local).AddTicks(5590), 1 });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ExerciseId", "ExerciseRepetitions" },
                values: new object[] { 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "ProgramId", "WorkoutId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "AddressId", "Disabilities", "GoalId", "Height", "MedicalConditions", "ProgramId", "SetId", "Weight", "WorkoutId" },
                values: new object[] { "keycloak-uid", 1, "none", 1, 171, "Anxiety", 1, 1, 60, 1 });

            migrationBuilder.InsertData(
                table: "WorkoutGoals",
                columns: new[] { "GoalId", "WorkoutId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "SetId", "WorkoutId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutGoals_WorkoutId",
                table: "WorkoutGoals",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_WorkoutId",
                table: "WorkoutPrograms",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSets_WorkoutId",
                table: "WorkoutSets",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutGoals");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutSets");

            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: "keycloak-uid");

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Sets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_WorkoutId",
                table: "Sets",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Workouts_WorkoutId",
                table: "Sets",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
