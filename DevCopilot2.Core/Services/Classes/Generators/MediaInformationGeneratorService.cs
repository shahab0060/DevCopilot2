using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class MediaInformationGeneratorService : IMediaInformationGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public MediaInformationGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion


        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}MediasInformation.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(entity)
            };
            generateFile.Code = $@"{GetUsings(entity)}

namespace {GetNameSpace(entity)}
{{
    public static class {entity.Entity.SingularName}MediaInformation
    {{

    {GetRegions(entity)}

    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }
        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Core\MediasInformation\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Core.MediasInformation.{entity.Entity.FolderName}";

        #region usings

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings(),
                $"{entity.Project.EnglishName}.Core.Utils",
                $"{entity.Project.EnglishName}.Domain.DTOs.Common"
                ];
            return usings.GetUsings(GetNameSpace(entity));
        }

        #endregion

        #region regions

        private string GetRegions(EntityFullInformationDto entity)
        {
            var fileProperties = entity
                .Properties
                .Where(a => a.IsAnyKindFile())
                .ToList();
            return string.Join("\n",
                fileProperties
                .ConvertAll(a => GetSinglePropertyMedias(a, entity.Entity)));
        }

        private List<PropertyImageResizeInformationListDto> GetSinglePropertyImageResizes(PropertyListDto property)
        {
            PropertyImageResizeInformationListDto originalMediaInformation = new PropertyImageResizeInformationListDto()
            {
                Name = "Original"
            };
            List<PropertyImageResizeInformationListDto> resizes = property
                .PropertyImageResizeInformationList
                .ToList();
            resizes.Add(originalMediaInformation);
            return resizes;
        }

        private string GetSinglePropertyMedias(PropertyListDto property, EntityListDto entity)
        {
            List<PropertyImageResizeInformationListDto> resizes = GetSinglePropertyImageResizes(property);
            string resizesCode = string.Join("\n", resizes
                .ConvertAll(a => GetSingleResize(a, property, entity)));
            return $@"
        #region {property.Name.AddSpacesBetweenCapitals().ToLower()} medias
    
        {resizesCode}    

        {GetPropertyResizesList(property)}

        #endregion
";
        }

        private string GetPropertyResizesList(PropertyListDto property)
        {
            List<PropertyImageResizeInformationListDto> resizes = GetSinglePropertyImageResizes(property);
            string resizesNames = string.Join("\n",
                resizes
                .ConvertAll(a => $@"
                          {a.Name}{property.Name},"))
                .ReplaceLastOccurrence(',', ' ');
            return $@"public static List<MediaInformationDto> {property.Name}MediasInformation = new List<MediaInformationDto>()
                      {{
                          {resizesNames}
                      }};";
        }

        private string GetSingleResize(PropertyImageResizeInformationListDto resize, PropertyListDto property, EntityListDto entity)
        {
            return $@"public static MediaInformationDto {resize.Name}{property.Name} = new MediaInformationDto()
        {{
            GetAddress = $""{{PathExtension.BaseGetPath}}images/{entity.PluralName.ToRegionName()}/{property.Name.ToRegionName()}/{resize.Name.ToRegionName()}/"",
            SetAddress = $""{{PathExtension.BaseSetPath}}images/{entity.PluralName.ToRegionName()}/{property.Name.ToRegionName()}/{resize.Name.ToRegionName()}/"",
            Height = {(resize.Width > 0 ? resize.Width : "null")},
            Width = {(resize.Height > 0 ? resize.Height : "null")}
        }};";
        }


        #endregion
    }
}
