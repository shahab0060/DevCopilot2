using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddReactJsLocationToProjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReactProjectLocation",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 21, 13, 39, 23, 633, DateTimeKind.Local).AddTicks(7874), new DateTime(2025, 1, 21, 13, 39, 23, 633, DateTimeKind.Local).AddTicks(7904) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactProjectLocation",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 20, 15, 17, 20, 505, DateTimeKind.Local).AddTicks(9414), new DateTime(2025, 1, 20, 15, 17, 20, 505, DateTimeKind.Local).AddTicks(9435) });
        }
    }
}
