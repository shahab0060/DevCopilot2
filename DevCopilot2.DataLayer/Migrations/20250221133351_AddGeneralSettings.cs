using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneralSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    DefaultSolutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSolutionLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GeneralSettings",
                columns: new[] { "Id", "CreateDate", "DefaultSolutionLocation", "DefaultSolutionName", "DeletedOn", "EditCounts", "IsDelete", "LatestEditDate" },
                values: new object[] { (byte)1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "", null, 0, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 2, 21, 17, 3, 49, 192, DateTimeKind.Local).AddTicks(6816), new DateTime(2025, 2, 21, 17, 3, 49, 192, DateTimeKind.Local).AddTicks(6835) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LatestEditDate" },
                values: new object[] { new DateTime(2025, 1, 25, 0, 58, 51, 981, DateTimeKind.Local).AddTicks(8452), new DateTime(2025, 1, 25, 0, 58, 51, 981, DateTimeKind.Local).AddTicks(8482) });
        }
    }
}
