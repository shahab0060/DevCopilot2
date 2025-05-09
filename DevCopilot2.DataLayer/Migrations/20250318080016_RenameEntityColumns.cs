using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntityColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "EnglishSingularName",
            table: "Entities",
            newName: "SingularName");

            migrationBuilder.RenameColumn(
            name: "EnglishPluralName",
            table: "Entities",
            newName: "PluralName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "SingularName",
            table: "Entities",
            newName: "EnglishSingularName");

            migrationBuilder.RenameColumn(
                name: "PluralName",
            table: "Entities",
                newName: "EnglishPluralName");
        }
    }
}
