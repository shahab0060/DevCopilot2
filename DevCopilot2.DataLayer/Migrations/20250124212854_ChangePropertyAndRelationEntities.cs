using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyAndRelationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExcludeFromListDto",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 25, 0, 58, 51, 981, DateTimeKind.Local).AddTicks(8452), new DateTime(2025, 1, 25, 0, 58, 51, 981, DateTimeKind.Local).AddTicks(8482) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcludeFromListDto",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 21, 13, 39, 23, 633, DateTimeKind.Local).AddTicks(7874), new DateTime(2025, 1, 21, 13, 39, 23, 633, DateTimeKind.Local).AddTicks(7904) });
        }
    }
}
