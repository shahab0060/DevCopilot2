using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsWebAndIsApiToEntitySelectedProjectAreaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasApi",
                table: "EntitySelectedProjectAreas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWeb",
                table: "EntitySelectedProjectAreas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 9, 22, 19, 36, 765, DateTimeKind.Local).AddTicks(9411), new DateTime(2025, 1, 9, 22, 19, 36, 765, DateTimeKind.Local).AddTicks(9489) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApi",
                table: "EntitySelectedProjectAreas");

            migrationBuilder.DropColumn(
                name: "HasWeb",
                table: "EntitySelectedProjectAreas");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 31, 18, 15, 56, 271, DateTimeKind.Local).AddTicks(3637), new DateTime(2024, 7, 31, 18, 15, 56, 271, DateTimeKind.Local).AddTicks(3652) });
        }
    }
}
