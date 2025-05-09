using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemplateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SingleImageHtml",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 18, 14, 16, 6, 640, DateTimeKind.Local).AddTicks(9682), new DateTime(2024, 7, 18, 14, 16, 6, 640, DateTimeKind.Local).AddTicks(9702) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SingleImageHtml",
                table: "Templates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 16, 18, 25, 17, 877, DateTimeKind.Local).AddTicks(4990), new DateTime(2024, 7, 16, 18, 25, 17, 877, DateTimeKind.Local).AddTicks(5002) });
        }
    }
}
