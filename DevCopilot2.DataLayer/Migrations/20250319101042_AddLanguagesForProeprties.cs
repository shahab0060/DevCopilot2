using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguagesForProeprties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PluralName",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PluralTitle",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "SingularName",
                table: "Properties",
                newName: "Name");


            migrationBuilder.AlterColumn<string>(
                name: "SingularTitle",
                table: "EntitySelectedLanguages",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PluralTitle",
                table: "EntitySelectedLanguages",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PropertySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySelectedLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySelectedLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertySelectedLanguages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_LanguageId",
                table: "PropertySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_PropertyId",
                table: "PropertySelectedLanguages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_Title",
                table: "PropertySelectedLanguages",
                column: "Title");


            migrationBuilder.Sql(@"
    INSERT INTO PropertySelectedLanguages (LanguageId,PropertyId, Title,EditCounts,CreateDate,LatestEditDate,IsDelete)
    SELECT 4, Id, SingularTitle,0,GETDATE(),GETDATE(),0
    FROM Properties
");

            migrationBuilder.DropColumn(
                name: "SingularTitle",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertySelectedLanguages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Properties",
                newName: "SingularName");

            migrationBuilder.AddColumn<string>(
                name: "PluralName",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PluralTitle",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SingularTitle",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SingularTitle",
                table: "EntitySelectedLanguages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "PluralTitle",
                table: "EntitySelectedLanguages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.Sql(@"
        UPDATE Properties
        SET SingularTitle = (
            SELECT Title
            FROM PropertySelectedLanguages
            WHERE PropertySelectedLanguages.PropertyId = Properties.Id
        ),
    ");
        }
    }
}
