using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAddToMenuPropertyToTheEntitiesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddToMenu",
                table: "Entities",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.Sql("UPDATE Entities SET AddToMenu = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddToMenu",
                table: "Entities");
        }
    }
}
