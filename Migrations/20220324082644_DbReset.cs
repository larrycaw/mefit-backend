using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.Migrations
{
    public partial class DbReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TargetMuscleGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VideoURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    MedicalConditions = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Disabilities = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseRepetitions = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Achieved = table.Column<bool>(type: "bit", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutPrograms_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutSets_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalWorkouts",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalWorkouts", x => new { x.WorkoutId, x.GoalId });
                    table.ForeignKey(
                        name: "FK_GoalWorkouts_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "TargetMuscleGroup", "VideoURL" },
                values: new object[,]
                {
                    { 1, "Biceps strong", "img", "Barbell curl", "biceps", ".mov" },
                    { 2, "Legs strong", "img", "Leg press", "legs", ".mov" },
                    { 3, "Chest strong", "img", "Push up", "upper body", ".mov" },
                    { 4, "Biceps strong", "img", "Isolation curl", "biceps", ".mov" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Disabilities", "Height", "MedicalConditions", "Weight" },
                values: new object[] { "keycloak-uid", "none", 171, "Anxiety", 60 });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "Upper body", "Bicep enhancement" },
                    { 2, "Whole body", "Strength building" },
                    { 3, "Whole body", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Arm day", "Strength" },
                    { 2, "Leg day", "Strength" },
                    { 3, "Running", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "Achieved", "ProfileId", "ProgramEndDate", "ProgramId" },
                values: new object[,]
                {
                    { 1, false, "keycloak-uid", new DateTime(2022, 3, 24, 9, 26, 44, 291, DateTimeKind.Local).AddTicks(9924), 1 },
                    { 2, true, "keycloak-uid", new DateTime(2022, 3, 24, 9, 26, 44, 294, DateTimeKind.Local).AddTicks(7363), 2 },
                    { 3, true, "keycloak-uid", new DateTime(2022, 3, 24, 9, 26, 44, 294, DateTimeKind.Local).AddTicks(7412), 2 }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ExerciseId", "ExerciseRepetitions" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 2, 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "ProgramId", "WorkoutId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "GoalWorkouts",
                columns: new[] { "GoalId", "WorkoutId", "Completed" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 2, false }
                });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "SetId", "WorkoutId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProfileId",
                table: "Goals",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalWorkouts_GoalId",
                table: "GoalWorkouts",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ExerciseId",
                table: "Sets",
                column: "ExerciseId");

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
                name: "GoalWorkouts");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutSets");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
