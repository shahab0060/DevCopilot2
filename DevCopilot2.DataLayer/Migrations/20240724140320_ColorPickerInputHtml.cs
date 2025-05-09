using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ColorPickerInputHtml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorPickerInputCode",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 24, 17, 33, 17, 432, DateTimeKind.Local).AddTicks(8755), new DateTime(2024, 7, 24, 17, 33, 17, 432, DateTimeKind.Local).AddTicks(8771) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorPickerInputCode",
                table: "Templates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 18, 14, 16, 6, 640, DateTimeKind.Local).AddTicks(9682), new DateTime(2024, 7, 18, 14, 16, 6, 640, DateTimeKind.Local).AddTicks(9702) });
        }
    }
}
