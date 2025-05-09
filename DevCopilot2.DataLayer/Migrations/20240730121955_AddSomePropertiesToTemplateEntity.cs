using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSomePropertiesToTemplateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BreadcrumbCode",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 30, 15, 49, 53, 853, DateTimeKind.Local).AddTicks(4541), new DateTime(2024, 7, 30, 15, 49, 53, 853, DateTimeKind.Local).AddTicks(4575) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreadcrumbCode",
                table: "Templates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 24, 17, 33, 17, 432, DateTimeKind.Local).AddTicks(8755), new DateTime(2024, 7, 24, 17, 33, 17, 432, DateTimeKind.Local).AddTicks(8771) });
        }
    }
}
