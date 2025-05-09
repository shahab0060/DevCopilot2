using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddButtonsInformationToTemplateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnchorTagCode",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubmitBtnCode",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 31, 18, 15, 56, 271, DateTimeKind.Local).AddTicks(3637), new DateTime(2024, 7, 31, 18, 15, 56, 271, DateTimeKind.Local).AddTicks(3652) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnchorTagCode",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "SubmitBtnCode",
                table: "Templates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2024, 7, 30, 15, 49, 53, 853, DateTimeKind.Local).AddTicks(4541), new DateTime(2024, 7, 30, 15, 49, 53, 853, DateTimeKind.Local).AddTicks(4575) });
        }
    }
}
