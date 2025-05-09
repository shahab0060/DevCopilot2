using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSuffixToTheLanguageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultPluralSuffix",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultPluralSuffix",
                table: "Languages");
        }
    }
}
