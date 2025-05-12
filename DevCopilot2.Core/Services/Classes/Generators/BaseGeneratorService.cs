using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Mappers.Entities;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class BaseGeneratorService : IBaseGeneratorService
    {
        #region constructor

        private readonly ICrudRepository<Entity, int> _entityRepository;
        public BaseGeneratorService(ICrudRepository<Entity, int> entityRepository)
        {
            this._entityRepository = entityRepository;
        }

        #endregion

        List<EntityFullInformationDto> catchedEntities = new List<EntityFullInformationDto>();

        public async Task<EntityFullInformationDto?> GetEntityFullInformation(long entityid)
        {
            if (catchedEntities.Any(a => a.Entity.Id == entityid))
                return catchedEntities.First(a => a.Entity.Id == entityid);
            _entityRepository.ClearChangeTracker();
            EntityFullInformationDto? entity = await _entityRepository
            .GetQueryable()
            .Where(a => a.Id == entityid && !a.IsExcluded)
            .ToFullDto()
            .FirstOrDefaultAsync();
            if (entity is null) return entity;
            entity = await SetFieldInRelationEntities(entity);
            catchedEntities.Add(entity);
            return entity;
        }

        async Task<EntityFullInformationDto> SetFieldInRelationEntities(EntityFullInformationDto entity)
        {
            //return entity;
            List<long> fieldRelationEntityIds = entity.GetFieldInRelationEntityIds();
            foreach (var relationEntityId in fieldRelationEntityIds)
            {
                EntityFullInformationDto? relationEntity = await GetEntityFullInformation(relationEntityId);
                if (relationEntity is null) continue;
                entity.FieldInRelationEntities.Add(relationEntity);
            }
            return entity;
        }

        public async Task<List<EntityFullInformationDto>> GetEntitiesFullInformation(long projectId)
        {
            List<EntityFullInformationDto> entities = await _entityRepository
            .GetQueryable()
            .Where(a => a.ProjectId == projectId && !a.IsExcluded)
            .ToFullDto()
            .ToListAsync();
            catchedEntities.AddRange(entities);
            for (int i = 0; i < entities.Count; i++)
            {
                EntityFullInformationDto? entity = entities[i];
                entity = await SetFieldInRelationEntities(entity);
                entities[i] = entity;
            }
            return entities;
        }

        public async Task<List<EntityFullInformationDto>> GetEntitiesFullInformation(string serviceName, long projectId)
        {
            List<EntityFullInformationDto> entities = await _entityRepository
            .GetQueryable()
            .Where(a => a.ServiceName == serviceName && a.ProjectId == projectId)
            .ToFullDto()
            .ToListAsync();
            for (int i = 0; i < entities.Count; i++)
            {
                EntityFullInformationDto? entity = entities[i];
                entity = await SetFieldInRelationEntities(entity);
                entities[i] = entity;
            }
            return entities;
        }


        public List<string> GetBaseUsings()
        {
            List<string> usings = ["System"];
            return usings;
        }


        public CreateFileResultDto GenerateFile(GenerateFileDto generate)
        {
            CreateFileResultDto result = new CreateFileResultDto()
            {
                Name = generate.FileNameWithExtension
            };
            try
            {
                string filePath = $@"{generate.Path}\{generate.FileNameWithExtension}";
                if (!generate.Override)
                    if (File.Exists(filePath))
                    {
                        result.FailedCount = 1;
                        result.FailedErrors.Add("File Already Exists and settings do not allow override");
                        return result;
                    }
                if (!Directory.Exists(generate.Path))
                    Directory.CreateDirectory(generate.Path);
                using (FileStream fs = File.Create(filePath))
                {
                    byte[] content = System.Text.Encoding.UTF8.GetBytes(generate.Code);
                    fs.Write(content, 0, content.Length);
                }
                FormatFile(filePath);
                result.CreatedCount = 1;
                return result;
            }
            catch (Exception e)
            {
                result.FailedCount = 1;
                result.FailedErrors.Add(e.Message);
                return result;
            }
        }

        public void FormatFile(string fileLocation)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(fileLocation);

                // Use a list to store the updated lines
                List<string> updatedLines = new List<string>();

                // Track the previous line to detect consecutive empty lines
                bool previousLineWasEmpty = false;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        if (!previousLineWasEmpty)
                        {
                            // Add a single empty line
                            updatedLines.Add(string.Empty);
                            previousLineWasEmpty = true;
                        }
                    }
                    else
                    {
                        // Add the non-empty line
                        updatedLines.Add(line);
                        previousLineWasEmpty = false;
                    }
                }

                // Write the updated lines back to the file
                File.WriteAllLines(fileLocation, updatedLines);
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public BaseChangeEntityResult AddRegionAndCode(string fileLocation, string regionName, string code,string regionNameToFind)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(fileLocation);
                if (lines.Any(a => a.Trim() == code.Trim())) return BaseChangeEntityResult.Invalid;
                // Find the first occurrence of "#region region name"
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Trim() == $"#region {regionNameToFind}")
                    {
                        for (int j = i + 1; j < lines.Length; j++)
                        {
                            if (lines[j].Trim() == $"#region {regionName}")
                            {
                                lines[j] += Environment.NewLine + code.Trim();
                                File.WriteAllLines(fileLocation, lines);
                                return BaseChangeEntityResult.Success;
                            }
                        }
                        string regionAndCode = $@"
        #region {regionName}
                
{code}        
    
        #endregion";
                        lines[i] += Environment.NewLine + regionAndCode;
                        File.WriteAllLines(fileLocation, lines);
                        return BaseChangeEntityResult.Success;
                    }
                }

                // If not found, return -1
                return BaseChangeEntityResult.Invalid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BaseChangeEntityResult.Invalid;
            }
        }

        public BaseChangeEntityResult AppendUsing(string fileLocation, string newUsing)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(fileLocation);

                // Split the newUsing string into individual lines
                string[] newUsings = newUsing.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                // Filter out the new using statements that are already present in the file
                var usingsToAdd = newUsings.Where(newUsingLine =>
                    !lines.Any(existingLine => existingLine.Trim() == newUsingLine.Trim())).ToList();

                if (usingsToAdd.Count == 0)
                {
                    // If all new using statements are already present, skip the modification
                    return BaseChangeEntityResult.Exists;
                }

                // Find the last occurrence of "using"
                int lastUsingIndex = Array.LastIndexOf(lines, lines.FirstOrDefault(line => line.Trim().StartsWith("using")));

                if (lastUsingIndex >= 0)
                {
                    // Insert the new "using" lines after the last one
                    var updatedLines = lines.ToList();
                    updatedLines.InsertRange(lastUsingIndex + 1, usingsToAdd);

                    // Write the updated lines back to the file
                    File.WriteAllLines(fileLocation, updatedLines);
                    return BaseChangeEntityResult.Success;
                }
            }
            catch (Exception ex)
            {
                return BaseChangeEntityResult.Invalid;
            }
            return BaseChangeEntityResult.NotFound;
        }


    }
}
