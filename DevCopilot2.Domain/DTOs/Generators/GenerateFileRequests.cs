using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Domain.DTOs.Generators
{
    public class EntityFullInformationDto
    {
        public EntityFullInformationDto(EntityFullInformationDto entity)
        {
            this.Entity = entity.Entity;
            this.Project = entity.Project;
            this.Properties = entity.Properties;
            this.Relations = entity.Relations;
            this.FieldInRelationEntities = entity.FieldInRelationEntities;
        }

        public EntityFullInformationDto()
        {
            
        }

        public EntityListDto Entity { get; set; } = new EntityListDto();

        public ProjectListDto Project { get; set; } = new ProjectListDto();
        public List<PropertyListDto> Properties { get; set; } = new List<PropertyListDto>();

        public List<EntityRelationListDto> Relations { get; set; } = new List<EntityRelationListDto>();

        public List<EntityFullInformationDto> FieldInRelationEntities { get; set; } = new List<EntityFullInformationDto>();

        public EntityFullInformationDto Clone()
            => new EntityFullInformationDto(this);
    }
}
