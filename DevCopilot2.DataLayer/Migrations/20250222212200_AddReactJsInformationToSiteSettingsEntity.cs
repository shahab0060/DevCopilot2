using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddReactJsInformationToSiteSettingsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultReactJsSolutionName",
                table: "GeneralSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultReactSolutionLocation",
                table: "GeneralSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GeneralSettings",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "DefaultReactJsSolutionName", "DefaultReactSolutionLocation" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 51, 57, 771, DateTimeKind.Local).AddTicks(9794), new DateTime(2025, 2, 23, 0, 51, 57, 771, DateTimeKind.Local).AddTicks(9896) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultReactJsSolutionName",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "DefaultReactSolutionLocation",
                table: "GeneralSettings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 2, 21, 17, 3, 49, 192, DateTimeKind.Local).AddTicks(6816), new DateTime(2025, 2, 21, 17, 3, 49, 192, DateTimeKind.Local).AddTicks(6835) });
        }
    }
}
