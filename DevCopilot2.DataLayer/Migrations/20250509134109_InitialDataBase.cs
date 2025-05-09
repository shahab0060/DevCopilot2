using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevCopilot2.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultSolutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSolutionLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultReactJsSolutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultReactSolutionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DefaultPluralSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMSTemplateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FavIconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumberActivationCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumberActivationCodeExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "RoleSelectedPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSelectedPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleSelectedPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleSelectedPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Architecture = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    ReactProjectLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "UserSelectedRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSelectedRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSelectedRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSelectedRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingularName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InheritedEntityId = table.Column<int>(type: "int", nullable: true),
                    IdType = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IsExcluded = table.Column<bool>(type: "bit", nullable: false),
                    AddToMenu = table.Column<bool>(type: "bit", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                name: "ProjectSelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSelectedLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSelectedLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectSelectedLanguages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    SingleImageHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorPickerInputCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreadCrumbCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnchorTagCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitBtnCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "EntitySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    SingularTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PluralTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ProjectEnumProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectEnumId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    RangeFrom = table.Column<int>(type: "int", nullable: true),
                    RangeTo = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ProjectEnumId = table.Column<int>(type: "int", nullable: true),
                    DataAnnotationDataType = table.Column<int>(type: "int", nullable: false),
                    IsUnique = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdatable = table.Column<bool>(type: "bit", nullable: false),
                    ShowInList = table.Column<bool>(type: "bit", nullable: false),
                    IsFilterContain = table.Column<bool>(type: "bit", nullable: false),
                    IsFilterEqual = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    UseEditor = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ForceMapperCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcludeFromListDto = table.Column<bool>(type: "bit", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "ProjectAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectAreas_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnumPropertySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ProjectEnumPropertyId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "EntityRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryPropertyId = table.Column<int>(type: "int", nullable: false),
                    SecondaryEntityId = table.Column<int>(type: "int", nullable: false),
                    MiddleEntityId = table.Column<int>(type: "int", nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
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
                name: "PropertySelectedLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "EntitySelectedProjectAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ProjectAreaId = table.Column<int>(type: "int", nullable: false),
                    HasIndex = table.Column<bool>(type: "bit", nullable: false),
                    HasCreate = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdate = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false),
                    HasApi = table.Column<bool>(type: "bit", nullable: false),
                    HasWeb = table.Column<bool>(type: "bit", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntitySelectedProjectAreaId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
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
                table: "GeneralSettings",
                columns: new[] { "Id", "CreateDate", "DefaultReactJsSolutionName", "DefaultReactSolutionLocation", "DefaultSolutionLocation", "DefaultSolutionName", "DeletedOn", "EditCounts", "IsDelete", "LatestEditDate" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "C:\\Users\\surface\\Projects\\BaseCleanArchitectureTemplate\\BaseCleanArchitectureTemplate.zip", "BaseCleanArchitectureTemplate", null, 0, false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "CreateDate", "DeletedOn", "EditCounts", "FavIconName", "IsDelete", "LatestEditDate", "LogoImageName", "SMSApiKey", "SMSTemplateName", "SiteName" },
                values: new object[] { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "favicon.ico", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "logo.png", "apiKey", "defaultVerification", "نام سایت شما" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeletedOn", "EditCounts", "FirstName", "ImageName", "IsDelete", "IsSuperAdmin", "LastName", "LatestEditDate", "Password", "PhoneNumber", "PhoneNumberActivationCode", "PhoneNumberActivationCodeExpireTime" },
                values: new object[] { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "نام", null, false, true, "نام خانوادگی", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12345678", "09121234567", "", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AuthorId",
                table: "Entities",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_CreateDate",
                table: "Entities",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_InheritedEntityId",
                table: "Entities",
                column: "InheritedEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_IsDelete",
                table: "Entities",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_LatestEditDate",
                table: "Entities",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ProjectId",
                table: "Entities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_CreateDate",
                table: "EntityRelations",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_IsDelete",
                table: "EntityRelations",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelations_LatestEditDate",
                table: "EntityRelations",
                column: "LatestEditDate");

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
                name: "IX_EntitySelectedLanguages_CreateDate",
                table: "EntitySelectedLanguages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_EntityId",
                table: "EntitySelectedLanguages",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_IsDelete",
                table: "EntitySelectedLanguages",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_LanguageId",
                table: "EntitySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedLanguages_LatestEditDate",
                table: "EntitySelectedLanguages",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_CreateDate",
                table: "EntitySelectedProjectAreas",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_EntityId",
                table: "EntitySelectedProjectAreas",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_IsDelete",
                table: "EntitySelectedProjectAreas",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_LatestEditDate",
                table: "EntitySelectedProjectAreas",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreas_ProjectAreaId",
                table: "EntitySelectedProjectAreas",
                column: "ProjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_CreateDate",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_EntitySelectedProjectAreaId",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "EntitySelectedProjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_IsDelete",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_LatestEditDate",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySelectedProjectAreaSelectedFilters_PropertyId",
                table: "EntitySelectedProjectAreaSelectedFilters",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_CreateDate",
                table: "GeneralSettings",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_IsDelete",
                table: "GeneralSettings",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_LatestEditDate",
                table: "GeneralSettings",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CreateDate",
                table: "Languages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_IsDelete",
                table: "Languages",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LatestEditDate",
                table: "Languages",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreateDate",
                table: "Permissions",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsDelete",
                table: "Permissions",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_LatestEditDate",
                table: "Permissions",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_CreateDate",
                table: "ProjectAreas",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_IsDelete",
                table: "ProjectAreas",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_LatestEditDate",
                table: "ProjectAreas",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_ProjectId",
                table: "ProjectAreas",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAreas_TemplateId",
                table: "ProjectAreas",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_CreateDate",
                table: "ProjectEnumProperties",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_IsDelete",
                table: "ProjectEnumProperties",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_LatestEditDate",
                table: "ProjectEnumProperties",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumProperties_ProjectEnumId",
                table: "ProjectEnumProperties",
                column: "ProjectEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_CreateDate",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_IsDelete",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_LanguageId",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_LatestEditDate",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnumPropertySelectedLanguages_ProjectEnumPropertyId",
                table: "ProjectEnumPropertySelectedLanguages",
                column: "ProjectEnumPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_AuthorId",
                table: "ProjectEnums",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_CreateDate",
                table: "ProjectEnums",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_IsDelete",
                table: "ProjectEnums",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_LatestEditDate",
                table: "ProjectEnums",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnums_ProjectId",
                table: "ProjectEnums",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreateDate",
                table: "Projects",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IsDelete",
                table: "Projects",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LatestEditDate",
                table: "Projects",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSelectedLanguages_CreateDate",
                table: "ProjectSelectedLanguages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSelectedLanguages_IsDelete",
                table: "ProjectSelectedLanguages",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSelectedLanguages_LanguageId",
                table: "ProjectSelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSelectedLanguages_LatestEditDate",
                table: "ProjectSelectedLanguages",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSelectedLanguages_ProjectId",
                table: "ProjectSelectedLanguages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CreateDate",
                table: "Properties",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_EntityId",
                table: "Properties",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_IsDelete",
                table: "Properties",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LatestEditDate",
                table: "Properties",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProjectEnumId",
                table: "Properties",
                column: "ProjectEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageResizeInformation_CreateDate",
                table: "PropertyImageResizeInformation",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageResizeInformation_IsDelete",
                table: "PropertyImageResizeInformation",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageResizeInformation_LatestEditDate",
                table: "PropertyImageResizeInformation",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageResizeInformation_PropertyId",
                table: "PropertyImageResizeInformation",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_CreateDate",
                table: "PropertySelectedLanguages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_IsDelete",
                table: "PropertySelectedLanguages",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_LanguageId",
                table: "PropertySelectedLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_LatestEditDate",
                table: "PropertySelectedLanguages",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectedLanguages_PropertyId",
                table: "PropertySelectedLanguages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreateDate",
                table: "Roles",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDelete",
                table: "Roles",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_LatestEditDate",
                table: "Roles",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSelectedPermissions_CreateDate",
                table: "RoleSelectedPermissions",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSelectedPermissions_IsDelete",
                table: "RoleSelectedPermissions",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSelectedPermissions_LatestEditDate",
                table: "RoleSelectedPermissions",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSelectedPermissions_PermissionId",
                table: "RoleSelectedPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSelectedPermissions_RoleId",
                table: "RoleSelectedPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_CreateDate",
                table: "SiteSettings",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_IsDelete",
                table: "SiteSettings",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_LatestEditDate",
                table: "SiteSettings",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_AuthorId",
                table: "Templates",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreateDate",
                table: "Templates",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_IsDelete",
                table: "Templates",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_LatestEditDate",
                table: "Templates",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ProjectId",
                table: "Templates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreateDate",
                table: "Users",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirstName",
                table: "Users",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDelete",
                table: "Users",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastName",
                table: "Users",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LatestEditDate",
                table: "Users",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber_Password",
                table: "Users",
                columns: new[] { "PhoneNumber", "Password" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber_PhoneNumberActivationCode_PhoneNumberActivationCodeExpireTime",
                table: "Users",
                columns: new[] { "PhoneNumber", "PhoneNumberActivationCode", "PhoneNumberActivationCodeExpireTime" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedRoles_CreateDate",
                table: "UserSelectedRoles",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedRoles_IsDelete",
                table: "UserSelectedRoles",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedRoles_LatestEditDate",
                table: "UserSelectedRoles",
                column: "LatestEditDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedRoles_RoleId",
                table: "UserSelectedRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSelectedRoles_UserId",
                table: "UserSelectedRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityRelations");

            migrationBuilder.DropTable(
                name: "EntitySelectedLanguages");

            migrationBuilder.DropTable(
                name: "EntitySelectedProjectAreaSelectedFilters");

            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropTable(
                name: "ProjectEnumPropertySelectedLanguages");

            migrationBuilder.DropTable(
                name: "ProjectSelectedLanguages");

            migrationBuilder.DropTable(
                name: "PropertyImageResizeInformation");

            migrationBuilder.DropTable(
                name: "PropertySelectedLanguages");

            migrationBuilder.DropTable(
                name: "RoleSelectedPermissions");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "UserSelectedRoles");

            migrationBuilder.DropTable(
                name: "EntitySelectedProjectAreas");

            migrationBuilder.DropTable(
                name: "ProjectEnumProperties");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

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
