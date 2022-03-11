using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.Migrations
{
    public partial class ExtraSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddressLine1", "AddressLine2", "AddressLine3", "City", "Contry", "PostalCode" },
                values: new object[] { "Hans Nielsen Hauges Gate 10", null, null, "Trondheim", "Norway", "7067" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "AddressLine3", "City", "Contry", "PostalCode" },
                values: new object[] { 2, "Høgreina 18c", null, null, "Trondheim", "Norway", "7079" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "TargetMuscleGroup", "VideoURL" },
                values: new object[,]
                {
                    { 2, "Legs strong", "img", "Leg press", "legs", ".mov" },
                    { 3, "Chest strong", "img", "Push up", "upper body", ".mov" },
                    { 4, "Biceps strong", "img", "Isolation curl", "biceps", ".mov" }
                });

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProgramEndDate",
                value: new DateTime(2022, 3, 11, 10, 24, 3, 269, DateTimeKind.Local).AddTicks(4592));

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 2, "Whole body", "Strength building" },
                    { 3, "Whole body", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Complete", "Name", "Type" },
                values: new object[,]
                {
                    { 2, true, "Leg day", "Strength" },
                    { 3, false, "Running", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "Achieved", "ProfileId", "ProgramEndDate", "ProgramId" },
                values: new object[,]
                {
                    { 2, true, "keycloak-uid", new DateTime(2022, 3, 11, 10, 24, 3, 277, DateTimeKind.Local).AddTicks(5592), 2 },
                    { 3, true, "keycloak-uid", new DateTime(2022, 3, 11, 10, 24, 3, 277, DateTimeKind.Local).AddTicks(5732), 2 }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ExerciseId", "ExerciseRepetitions" },
                values: new object[] { 2, 2, 20 });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "ProgramId", "WorkoutId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "SetId", "WorkoutId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "WorkoutGoals",
                columns: new[] { "GoalId", "WorkoutId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "SetId", "WorkoutId" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkoutGoals",
                keyColumns: new[] { "GoalId", "WorkoutId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "WorkoutPrograms",
                keyColumns: new[] { "ProgramId", "WorkoutId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "WorkoutPrograms",
                keyColumns: new[] { "ProgramId", "WorkoutId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "WorkoutPrograms",
                keyColumns: new[] { "ProgramId", "WorkoutId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "WorkoutSets",
                keyColumns: new[] { "SetId", "WorkoutId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "WorkoutSets",
                keyColumns: new[] { "SetId", "WorkoutId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddressLine1", "AddressLine2", "AddressLine3", "City", "Contry", "PostalCode" },
                values: new object[] { "eef", "fkr", "efe", "jfr", "fw4w", "1123" });

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProgramEndDate",
                value: new DateTime(2022, 3, 11, 9, 46, 11, 159, DateTimeKind.Local).AddTicks(61));
        }
    }
}
