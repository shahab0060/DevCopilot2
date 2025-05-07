using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot2.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneralSettingsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GeneralSettings",
                columns: new[] { "Id", "CreateDate", "DefaultReactJsSolutionName", "DefaultReactSolutionLocation", "DefaultSolutionLocation", "DefaultSolutionName", "DeletedOn", "EditCounts", "IsDelete", "LatestEditDate" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "C:\\Users\\surface\\Projects\\BaseCleanArchitectureTemplate\\BaseCleanArchitectureTemplate.zip", "BaseCleanArchitectureTemplate", null, 0, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralSettings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
