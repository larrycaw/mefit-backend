using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.Migrations
{
    public partial class UpdateGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Goals");

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ProfileId", "ProgramEndDate" },
                values: new object[] { "keycloak-uid", new DateTime(2022, 3, 11, 9, 46, 11, 159, DateTimeKind.Local).AddTicks(61) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ProfileId", "ProgramEndDate", "Userid" },
                values: new object[] { null, new DateTime(2022, 3, 11, 9, 43, 0, 861, DateTimeKind.Local).AddTicks(2532), "keycloak-uid" });
        }
    }
}
