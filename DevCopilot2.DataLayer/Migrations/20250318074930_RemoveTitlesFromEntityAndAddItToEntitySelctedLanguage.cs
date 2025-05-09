using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTitlesFromEntityAndAddItToEntitySelctedLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "EntitySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    SingularTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySelectedLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitySelectedLanguages_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySelectedLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_EntityId",
                table: "EntitySelectedLanguages",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_LanguageId",
                table: "EntitySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.Sql(@"
    INSERT INTO EntitySelectedLanguages (LanguageId,EntityId, SingularTitle,PluralTitle,EditCounts,CreateDate,LatestEditDate,IsDelete)
    SELECT 4, Id, SingularTitle, PluralTitle,0,GETDATE(),GETDATE(),0
    FROM Entities
");

            migrationBuilder.DropColumn(
                name: "PluralTitle",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "SingularTitle",
                table: "Entities");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntitySelectedLanguages");

            migrationBuilder.AddColumn<string>(
                name: "PluralTitle",
                table: "Entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SingularTitle",
                table: "Entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
        UPDATE Entities
        SET SingularTitle = (
            SELECT SingularTitle
            FROM EntitySelectedLanguages
            WHERE EntitySelectedLanguages.EntityId = Entities.Id
        ),
        PluralTitle = (
            SELECT PluralTitle
            FROM EntitySelectedLanguages
            WHERE EntitySelectedLanguages.EntityId = Entities.Id
        )
    ");
        }
    }
}
