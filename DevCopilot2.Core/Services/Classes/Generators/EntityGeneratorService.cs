using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.Enums.Relations;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class EntityGeneratorService : IEntityGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public EntityGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public async Task<CreateFileResultDto> Generate(GenerateEntityDto generate)
        {
            EntityFullInformationDto? entity = await _baseGeneratorService.GetEntityFullInformation(generate.EntityId);
            if (entity is null) return new CreateFileResultDto()
            {
                FailedCount = 1,
                Name = $"entity with id : {generate.EntityId}",
                FailedErrors = new List<string>()
                {
                    "Entity Not Found in the database"
                }
            };
            return Generate(generate, entity);
        }
        public CreateFileResultDto Generate(GenerateEntityDto generateEntity, EntityFullInformationDto entity)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}.cs",
                Override = generateEntity.OverrideFiles,
                Path = GetFilePath(entity)
            };
            generateFile.Code = $@"{GetUsings(entity)}

{GetIndexAttributes(entity)}
namespace {GetNameSpace(entity)}
{{
    public partial class {entity.Entity.SingularName} : EntityId<{entity.Entity.GetDataType()}>
    {{
{GetPropertiesCode(entity)}

{GetRelationsCode(entity)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{entity.Project.EnglishName}.Domain.Entities.Common");
            if (entity.Project.UseResources)
                usings.Add($"{entity.Project.EnglishName}.Domain.Resources.DTOs.Common");
            if (!entity.Project.UseFluent)
                usings.Add($"System.ComponentModel.DataAnnotations");
            if (entity.HasRelations())
            {
                usings.Add("Microsoft.EntityFrameworkCore");
                usings.AddRange(entity.Relations.Select(a => $"{entity.Project.EnglishName}.Domain.Entities.{a.PrimaryPropertyEntityFolderName}"));
                if (entity
                    .GetRelationProperties()
                    .Any())
                {
                    usings.Add("System.ComponentModel.DataAnnotations.Schema");
                    usings.AddRange(entity.GetRelationProperties()
                        .ConvertAll(b => $"{entity.Project.EnglishName}.Domain.Entities.{b.EntityRelation!.SecondaryEntity.FolderName}"));
                }
            }
            usings.AddRange(entity.GetEnumUsings());
            return usings.GetUsings(GetNameSpace(entity));
        }

        string GetIndexAttributes(EntityFullInformationDto entity)
        {
            if (!entity.Project.UseFluent) return string.Empty;
            List<PropertyListDto> properties = entity.GetIndexableProperties();
            return string.Join("\n",
                properties
                .ConvertAll(a =>
                $@"[Index(nameof({a.Name}){(a.IsUnique ? ", IsUnique = true" : "")})]"));
        }

        string GetPropertiesCode(EntityFullInformationDto entity)
        {
            string properties = string.Join("\n", entity
                .Properties
                .ConvertAll(a => GetSinglePropertyCode(a)));
            return $@"
        #region properties

{properties}

        #endregion";
        }

        string GetSinglePropertyCode(PropertyListDto property)
        {
            string propertyNullDetection = string.Empty;
            if (property.DataType == DataTypeEnum.String)
                propertyNullDetection = "= null!;";
            return $@"       
        [Display(Name = ""{property.Name}"")]
{property.GetDataAnnotationsCode()}
        public {property.GetDataType()} {property.Name} {{ get; set; }} {propertyNullDetection}
";
        }

        string GetRelationsCode(EntityFullInformationDto entity)
        {
            if (!entity.HasRelations()) return string.Empty;
            List<EntityRelationListDto> entityRelations = entity.Relations
                .Where(a => a.RelationType == RelationTypeEnum.OneToMany)
                .ToList();
            var entityRelationsCode = string.Join('\n', entityRelations
                .ConvertAll(a => GetSingleEntityRelation(a, entityRelations)));
            List<PropertyListDto> propertiesWithRelation = entity.GetRelationProperties();
            var propertiesRelationsCode = string.Join("\n",
                propertiesWithRelation
                .ConvertAll(a => $@"        [ForeignKey(nameof({a.Name}))]
        public virtual {a.EntityRelation!.SecondaryEntity.SingularName}{(a.IsRequired ? string.Empty : "?")} {a.GetRelationPropertyName()} {{ get; set; }} = null!;
"));
            return $@"
        #region relations

{entityRelationsCode}

{propertiesRelationsCode}

        #endregion";
        }

        public string GetSingleEntityRelation(EntityRelationListDto relation, List<EntityRelationListDto> entityRelations)
        {
            if (entityRelations.Count(b => b.PrimaryPropertyEntityTitle == relation.PrimaryPropertyEntityTitle) > 1)
            {
                return $@"        [InverseProperty(""{relation.PrimaryPropertyTitle.GetRelationPropertyName()}"")]
        public virtual ICollection<{relation.PrimaryPropertyEntityTitle}>? {relation.PrimaryPropertyTitle.GetRelationPropertyName()}List {{ get; set; }} = new List<{relation.PrimaryPropertyEntityTitle}>();
";
            }
            return $@"        public virtual ICollection<{relation.PrimaryPropertyEntityTitle}> {relation.PrimaryPropertyEntityPluralTitle} {{ get; set; }} = new List<{relation.PrimaryPropertyEntityTitle}>();
";
        }

        public string GetNameSpace(EntityFullInformationDto entity)
        => $@"{entity.Project.EnglishName}.Domain.Entities.{entity.Entity.FolderName}";

        public string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Domain\Entities\{entity.Entity.FolderName}";
    }
}
