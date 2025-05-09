using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Mappers.Generate
{
    public static class GenerateMappers
    {
        public static GenerateEntityDto ToGenerate(this BaseGeneratorDto g)
            => new GenerateEntityDto()
            {
                AddToDbContext = g.AddToDbContext,
                GenerateEntities = g.GenerateEntities,
                GenerateDtos = g.GenerateDtos,
                GenerateMediasInformation = g.GenerateMediasInformation,
                GenerateMappers = g.GenerateMappers,
                GenerateEnums = g.GenerateEnums,
                GenerateServices = g.GenerateServices,
                GenerateIServices = g.GenerateIServices,
                GenerateWebControllers = g.GenerateWebControllers,
                GenerateWebViews = g.GenerateWebViews,
                GenerateApiControllers = g.GenerateApiControllers,
                GenerateControllerResources = g.GenerateControllerResources,
                GenerateMenus = g.GenerateMenus,
                GenerateResources = g.GenerateResources,
                GenerateViewResources = g.GenerateViewResources
            };

    }
}
