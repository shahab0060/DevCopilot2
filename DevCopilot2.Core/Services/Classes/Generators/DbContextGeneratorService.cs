using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class DbContextGeneratorService : IDbContextGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public DbContextGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public void AddEntityDbSet(EntityFullInformationDto entity)
        {
            string location = $@"{entity.Project.EnglishName}.DataLayer\Context";
            string fileName = $"DbContext";
            string fullLocation = $@"{entity.Project.Location}\{location}\{fileName}.cs";
            string dbSetCode = $@"        public virtual DbSet<{entity.Entity.SingularName}> {entity.Entity.PluralName} {{ get; set; }}";
            _baseGeneratorService.AddRegionAndCode(fullLocation, entity.Entity.FolderName.AddSpacesBetweenCapitals().ToLower(), dbSetCode, "entities");
            string usingCode = $@"using {entity.Project.EnglishName}.Domain.Entities.{entity.Entity.FolderName};";
            _baseGeneratorService.AppendUsing(fullLocation, usingCode);
            _baseGeneratorService.FormatFile(fullLocation);
        }

        public void AddPermissionsSeedData(List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto? firstEntity = entities.FirstOrDefault();
            if (firstEntity is null) return;
            string location = $@"{firstEntity.Project.EnglishName}.DataLayer\Context";
            string fileName = $"DbContext";
            string fullLocation = $@"{firstEntity.Project.Location}\{location}\{fileName}.cs";
            int index = 0;
            string permissionsSeedDataCode = string.Join("\n",
                            entities
                            .ConvertAll(a => $@"
            new Permission()
            {{
                Id = {index += 1},
                CreateDate = new DateTime(2025,01,01),
                KeyName = ""{a.Entity.PluralName}Management"",
                LatestEditDate = new DateTime(2025,01,01),
                Title = ""{a.Entity.PluralName}Management""
            }},"));
            permissionsSeedDataCode = permissionsSeedDataCode.ReplaceLastOccurrence(',', ' ');
            string seedDataFinalCode = $@"
            
            modelBuilder.Entity<Permission>().HasData({permissionsSeedDataCode});
"; _baseGeneratorService.AddRegionAndCode(fullLocation, "permissions".ToLower(), seedDataFinalCode, "seed data");
            _baseGeneratorService.FormatFile(fullLocation);
        }

    }
}
