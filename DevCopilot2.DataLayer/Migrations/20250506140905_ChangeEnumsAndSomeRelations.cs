using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot2.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEnumsAndSomeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Users_AuthorId",
                table: "Entities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnumProperties_Users_AuthorId",
                table: "ProjectEnumProperties");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEnumProperties_AuthorId",
                table: "ProjectEnumProperties");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ProjectEnumProperties");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Entities",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Users_AuthorId",
                table: "Entities",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Users_AuthorId",
                table: "Entities");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "ProjectEnumProperties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Entities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_AuthorId",
                table: "ProjectEnumProperties",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Users_AuthorId",
                table: "Entities",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnumProperties_Users_AuthorId",
                table: "ProjectEnumProperties",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
