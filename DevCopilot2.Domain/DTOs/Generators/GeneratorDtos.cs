using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DevCopilot2.Domain.DTOs.Generators
{
    public class BaseGeneratorDto
    {
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "OverrideFiles")]
        public bool OverrideFiles { get; set; } = true;

        [Display(Name = "GenerateEntities")]
        public bool GenerateEntities { get; set; }

        [Display(Name = "GenerateDtos")]
        public bool GenerateDtos { get; set; }

        [Display(Name = "GenerateMediasInformation")]
        public bool GenerateMediasInformation { get; set; }

        [Display(Name = "GenerateMappers")]
        public bool GenerateMappers { get; set; }

        [Display(Name = "GenerateEnums")]
        public bool GenerateEnums { get; set; }


        [Display(Name = "GenerateServices")]
        public bool GenerateServices { get; set; }

        [Display(Name = "GenerateIServices")]
        public bool GenerateIServices { get; set; }

        [Display(Name = "GenerateResources")]
        public bool GenerateResources { get; set; }

        [Display(Name = "AddToDbContext")]
        public bool AddToDbContext { get; set; }

        [Display(Name = "GenerateWebControllers")]
        public bool GenerateWebControllers { get; set; }

        [Display(Name = "GenerateWebViews")]
        public bool GenerateWebViews { get; set; }

        [Display(Name = "GenerateApiControllers")]
        public bool GenerateApiControllers { get; set; }

        [Display(Name = "GenerateMenus")]
        public bool GenerateMenus { get; set; }

        [Display(Name = "GenerateViewResources")]
        public bool GenerateViewResources { get; set; }

        [Display(Name = "GenerateControllerResources")]
        public bool GenerateControllerResources { get; set; }

        [Display(Name = "GeneratePermissionsSeedData")]
        public bool GeneratePermissionsSeedData { get; set; }
    }

    public class GenerateEntityDto : BaseGeneratorDto
    {
        [Display(Name = "Entity")]

        public int EntityId { get; set; }

        public GenerateEntityDto SetFullControl()
        {
            return new GenerateEntityDto()
            {
                GenerateEntities = true,
                GenerateDtos = true,
                GenerateMediasInformation = true,
                GenerateMappers = true,
                GenerateEnums = true,
                GenerateIServices = true,
                GenerateServices = true,
                AddToDbContext = true,
                GenerateWebControllers = true,
                GenerateWebViews = true,
                GenerateApiControllers = true,
                GenerateControllerResources = true,
                GenerateViewResources = true,
                GenerateMenus = true,
                GenerateResources = true,
                OverrideFiles = true,
                GeneratePermissionsSeedData = true
            };
        }
    }

    public class GenerateProjectDto : BaseGeneratorDto
    {
        [Display(Name = "GenerateSharedResources")]
        public bool GenerateSharedResources { get; set; }

        [Display(Name = "GenerateEnumsResources")]
        public bool GenerateEnumsResources { get; set; }

        public GenerateProjectDto SetFullControl()
        {
            return new GenerateProjectDto()
            {
                GenerateEntities = true,
                GenerateDtos = true,
                GenerateMediasInformation = true,
                GenerateMappers = true,
                GenerateEnums = true,
                GenerateIServices = true,
                GenerateServices = true,
                AddToDbContext = true,
                GenerateWebControllers = true,
                GenerateWebViews = true,
                GenerateApiControllers = true,
                GenerateControllerResources = true,
                GenerateViewResources = true,
                GenerateEnumsResources = true,
                GenerateMenus = true,
                GenerateResources = true,
                GenerateSharedResources = true,
                OverrideFiles = true,
                GeneratePermissionsSeedData = true
            };
        }
    }

}
