using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddForceMapperCodeToPropertyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForceMapperCode",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 20, 15, 17, 20, 505, DateTimeKind.Local).AddTicks(9414), new DateTime(2025, 1, 20, 15, 17, 20, 505, DateTimeKind.Local).AddTicks(9435) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForceMapperCode",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 14, 23, 30, 7, 193, DateTimeKind.Local).AddTicks(4158), new DateTime(2025, 1, 14, 23, 30, 7, 193, DateTimeKind.Local).AddTicks(4176) });
        }
    }
}
