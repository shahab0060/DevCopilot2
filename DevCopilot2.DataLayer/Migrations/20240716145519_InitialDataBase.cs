using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Architecture = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingularName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SingularTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InheritedEntityId = table.Column<long>(type: "bigint", nullable: true),
                    IdType = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Entities_InheritedEntityId",
                        column: x => x.InheritedEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entities_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnums",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEnums_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectEnums_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ListViewHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListFirstThCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListOtherThCodes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListBoolTdHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListTextTdHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListImageTdHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPriceTdHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListDefaultTdCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListViewCardHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatePageHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckBoxInputCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileInputCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextInputHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEditorInputHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegerInputHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectInputHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Templates_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnumProperties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectEnumId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnumProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEnumProperties_ProjectEnums_ProjectEnumId",
                        column: x => x.ProjectEnumId,
                        principalTable: "ProjectEnums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEnumProperties_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingularTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SingularName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    RangeFrom = table.Column<int>(type: "int", nullable: true),
                    RangeTo = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ProjectEnumId = table.Column<long>(type: "bigint", nullable: true),
                    DataAnnotationDataType = table.Column<int>(type: "int", nullable: false),
                    IsUnique = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdatable = table.Column<bool>(type: "bit", nullable: false),
                    ShowInList = table.Column<bool>(type: "bit", nullable: false),
                    IsFilterContain = table.Column<bool>(type: "bit", nullable: false),
                    IsFilterEqual = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UseEditor = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_ProjectEnums_ProjectEnumId",
                        column: x => x.ProjectEnumId,
                        principalTable: "ProjectEnums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Properties_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAreas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAreas_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectAreas_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryPropertyId = table.Column<long>(type: "bigint", nullable: false),
                    SecondaryEntityId = table.Column<long>(type: "bigint", nullable: false),
                    MiddleEntityId = table.Column<long>(type: "bigint", nullable: true),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    InputType = table.Column<int>(type: "int", nullable: false),
                    FillingType = table.Column<int>(type: "int", nullable: false),
                    FillingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityRelations_Entities_MiddleEntityId",
                        column: x => x.MiddleEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntityRelations_Entities_SecondaryEntityId",
                        column: x => x.SecondaryEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityRelations_Properties_PrimaryPropertyId",
                        column: x => x.PrimaryPropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImageResizeInformation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImageResizeInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImageResizeInformation_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySelectedProjectAreas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectAreaId = table.Column<long>(type: "bigint", nullable: false),
                    HasIndex = table.Column<bool>(type: "bit", nullable: false),
                    HasCreate = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdate = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySelectedProjectAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitySelectedProjectAreas_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySelectedProjectAreas_ProjectAreas_ProjectAreaId",
                        column: x => x.ProjectAreaId,
                        principalTable: "ProjectAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySelectedProjectAreaSelectedFilters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntitySelectedProjectAreaId = table.Column<long>(type: "bigint", nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySelectedProjectAreaSelectedFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitySelectedProjectAreaSelectedFilters_EntitySelectedProjectAreas_EntitySelectedProjectAreaId",
                        column: x => x.EntitySelectedProjectAreaId,
                        principalTable: "EntitySelectedProjectAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySelectedProjectAreaSelectedFilters_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeletedOn", "EditCounts", "IsDelete", "IsSuperAdmin", "LastName", "LatestEditDate", "Name", "Password", "UserName" },
                values: new object[] { 1L, new DateTime(2024, 7, 16, 18, 25, 17, 877, DateTimeKind.Local).AddTicks(4990), null, 0, false, true, "بختیاری", new DateTime(2024, 7, 16, 18, 25, 17, 877, DateTimeKind.Local).AddTicks(5002), "شهاب", "ShahabShahab", "ItsShaab" });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AuthorId",
                table: "Entities",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_InheritedEntityId",
                table: "Entities",
                column: "InheritedEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ProjectId",
                table: "Entities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_MiddleEntityId",
                table: "EntityRelations",
                column: "MiddleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_PrimaryPropertyId",
                table: "EntityRelations",
                column: "PrimaryPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_SecondaryEntityId",
                table: "EntityRelations",
                column: "SecondaryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_EntityId",
                table: "EntitySelectedProjectAreas",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_ProjectAreaId",
                table: "EntitySelectedProjectAreas",
                column: "ProjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_EntitySelectedProjectAreaId",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "EntitySelectedProjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_PropertyId",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_ProjectId",
                table: "ProjectAreas",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_TemplateId",
                table: "ProjectAreas",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_AuthorId",
                table: "ProjectEnumProperties",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_ProjectEnumId",
                table: "ProjectEnumProperties",
                column: "ProjectEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_AuthorId",
                table: "ProjectEnums",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_ProjectId",
                table: "ProjectEnums",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AuthorId",
                table: "Properties",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_EntityId",
                table: "Properties",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProjectEnumId",
                table: "Properties",
                column: "ProjectEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageResizeInformation_PropertyId",
                table: "PropertyImageResizeInformation",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_AuthorId",
                table: "Templates",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ProjectId",
                table: "Templates",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityRelations");

            migrationBuilder.DropTable(
                name: "EntitySelectedProjectAreaSelectedFilters");

            migrationBuilder.DropTable(
                name: "ProjectEnumProperties");

            migrationBuilder.DropTable(
                name: "PropertyImageResizeInformation");

            migrationBuilder.DropTable(
                name: "EntitySelectedProjectAreas");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "ProjectAreas");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "ProjectEnums");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
