using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot2.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuthorIdFromProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_AuthorId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AuthorId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AuthorId",
                table: "Properties",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_AuthorId",
                table: "Properties",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
