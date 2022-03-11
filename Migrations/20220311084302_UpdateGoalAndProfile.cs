using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.Migrations
{
    public partial class UpdateGoalAndProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Goals_GoalId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_GoalId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Goals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ProgramEndDate", "Userid" },
                values: new object[] { new DateTime(2022, 3, 11, 9, 43, 0, 861, DateTimeKind.Local).AddTicks(2532), "keycloak-uid" });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProfileId",
                table: "Goals",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Profiles_ProfileId",
                table: "Goals",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Profiles_ProfileId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ProfileId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProgramEndDate",
                value: new DateTime(2022, 3, 10, 14, 4, 22, 660, DateTimeKind.Local).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: "keycloak-uid",
                column: "GoalId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_GoalId",
                table: "Profiles",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Goals_GoalId",
                table: "Profiles",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
