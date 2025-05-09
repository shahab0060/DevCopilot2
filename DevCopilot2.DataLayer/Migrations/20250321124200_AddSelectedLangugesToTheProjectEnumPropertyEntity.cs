using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedLangugesToTheProjectEnumPropertyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectEnumPropertySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectEnumPropertyId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnumPropertySelectedLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEnumPropertySelectedLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEnumPropertySelectedLanguages_ProjectEnumProperties_ProjectEnumPropertyId",
                        column: x => x.ProjectEnumPropertyId,
                        principalTable: "ProjectEnumProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_LanguageId",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_ProjectEnumPropertyId",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "ProjectEnumPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_Title",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "Title");

            migrationBuilder.Sql(@"
    INSERT INTO ProjectEnumPropertySelectedLanguages (LanguageId,ProjectEnumPropertyId, Title,EditCounts,CreateDate,LatestEditDate,IsDelete)
    SELECT 4, Id, Title,0,GETDATE(),GETDATE(),0
    FROM ProjectEnumProperties
");
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProjectEnumProperties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectEnumPropertySelectedLanguages");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProjectEnumProperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
        UPDATE ProjectEnumProperties
        SET Title = (
            SELECT Title
            FROM ProjectEnumPropertySelectedLanguages
            WHERE ProjectEnumPropertySelectedLanguages.ProjectEnumPropertyId = ProjectEnumProperties.Id
        ),
    ");
        }
    }
}
