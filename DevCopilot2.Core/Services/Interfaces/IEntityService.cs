using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Core.Mappers.Properties;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Core.Mappers.Entities;
using DevCopilot2.Domain.DTOs.Entities;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IEntityService : IService
    {

        #region Properties

        #region property selected languages

		Task<FilterPropertySelectedLanguagesDto> FilterPropertySelectedLanguages(FilterPropertySelectedLanguagesDto filter);
        Task<List<ComboDto>> GetPropertySelectedLanguagesAsCombo(FilterPropertySelectedLanguagesDto filter);
		Task<PropertySelectedLanguageListDto?> GetSinglePropertySelectedLanguageInformation(int propertySelectedLanguageId);

        		Task<BaseChangeEntityResult> CreatePropertySelectedLanguage(CreatePropertySelectedLanguageDto create);
		Task<UpdatePropertySelectedLanguageDto?> GetPropertySelectedLanguageInformation(int propertySelectedLanguageId);
		Task<BaseChangeEntityResult> UpdatePropertySelectedLanguage(UpdatePropertySelectedLanguageDto update);
		Task<BaseChangeEntityResult> DeletePropertySelectedLanguage(int propertySelectedLanguageId);
		Task DeletePropertySelectedLanguage(List<int> propertySelectedLanguagesId);

        #endregion

        #region property image resize information

		Task<FilterPropertyImageResizeInformationDto> FilterPropertyImageResizeInformation(FilterPropertyImageResizeInformationDto filter);
        Task<List<ComboDto>> GetPropertyImageResizeInformationAsCombo(FilterPropertyImageResizeInformationDto filter);
		Task<PropertyImageResizeInformationListDto?> GetSinglePropertyImageResizeInformationInformation(int propertyImageResizeInformationId);

        		Task<BaseChangeEntityResult> CreatePropertyImageResizeInformation(CreatePropertyImageResizeInformationDto create);
		Task<UpdatePropertyImageResizeInformationDto?> GetPropertyImageResizeInformationInformation(int propertyImageResizeInformationId);
		Task<BaseChangeEntityResult> UpdatePropertyImageResizeInformation(UpdatePropertyImageResizeInformationDto update);
		Task<BaseChangeEntityResult> DeletePropertyImageResizeInformation(int propertyImageResizeInformationId);
		Task DeletePropertyImageResizeInformation(List<int> propertyImageResizeInformationId);

        #endregion

        #region properties

		Task<FilterPropertiesDto> FilterProperties(FilterPropertiesDto filter);
        Task<List<ComboDto>> GetPropertiesAsCombo(FilterPropertiesDto filter);
		Task<PropertyListDto?> GetSinglePropertyInformation(int propertyId);

        		Task<BaseChangeEntityResult> CreateProperty(CreatePropertyDto create);
		Task<UpdatePropertyDto?> GetPropertyInformation(int propertyId);
		Task<BaseChangeEntityResult> UpdateProperty(UpdatePropertyDto update);
		Task<BaseChangeEntityResult> DeleteProperty(int propertyId);
		Task DeleteProperty(List<int> propertiesId);

        #endregion

        #endregion

        #region Entities

        #region entity selected languages

		Task<FilterEntitySelectedLanguagesDto> FilterEntitySelectedLanguages(FilterEntitySelectedLanguagesDto filter);
        Task<List<ComboDto>> GetEntitySelectedLanguagesAsCombo(FilterEntitySelectedLanguagesDto filter);
		Task<EntitySelectedLanguageListDto?> GetSingleEntitySelectedLanguageInformation(int entitySelectedLanguageId);

        		Task<BaseChangeEntityResult> CreateEntitySelectedLanguage(CreateEntitySelectedLanguageDto create);
		Task<UpdateEntitySelectedLanguageDto?> GetEntitySelectedLanguageInformation(int entitySelectedLanguageId);
		Task<BaseChangeEntityResult> UpdateEntitySelectedLanguage(UpdateEntitySelectedLanguageDto update);
		Task<BaseChangeEntityResult> DeleteEntitySelectedLanguage(int entitySelectedLanguageId);
		Task DeleteEntitySelectedLanguage(List<int> entitySelectedLanguagesId);

        #endregion

        #region entity selected project area selected filters

		Task<FilterEntitySelectedProjectAreaSelectedFiltersDto> FilterEntitySelectedProjectAreaSelectedFilters(FilterEntitySelectedProjectAreaSelectedFiltersDto filter);
        Task<List<ComboDto>> GetEntitySelectedProjectAreaSelectedFiltersAsCombo(FilterEntitySelectedProjectAreaSelectedFiltersDto filter);
		Task<EntitySelectedProjectAreaSelectedFilterListDto?> GetSingleEntitySelectedProjectAreaSelectedFilterInformation(int entitySelectedProjectAreaSelectedFilterId);

        		Task<BaseChangeEntityResult> CreateEntitySelectedProjectAreaSelectedFilter(CreateEntitySelectedProjectAreaSelectedFilterDto create);
		Task<UpdateEntitySelectedProjectAreaSelectedFilterDto?> GetEntitySelectedProjectAreaSelectedFilterInformation(int entitySelectedProjectAreaSelectedFilterId);
		Task<BaseChangeEntityResult> UpdateEntitySelectedProjectAreaSelectedFilter(UpdateEntitySelectedProjectAreaSelectedFilterDto update);
		Task<BaseChangeEntityResult> DeleteEntitySelectedProjectAreaSelectedFilter(int entitySelectedProjectAreaSelectedFilterId);
		Task DeleteEntitySelectedProjectAreaSelectedFilter(List<int> entitySelectedProjectAreaSelectedFiltersId);

        #endregion

        #region entity selected project areas

		Task<FilterEntitySelectedProjectAreasDto> FilterEntitySelectedProjectAreas(FilterEntitySelectedProjectAreasDto filter);
        Task<List<ComboDto>> GetEntitySelectedProjectAreasAsCombo(FilterEntitySelectedProjectAreasDto filter);
		Task<EntitySelectedProjectAreaListDto?> GetSingleEntitySelectedProjectAreaInformation(int entitySelectedProjectAreaId);

        		Task<BaseChangeEntityResult> CreateEntitySelectedProjectArea(CreateEntitySelectedProjectAreaDto create);
		Task<UpdateEntitySelectedProjectAreaDto?> GetEntitySelectedProjectAreaInformation(int entitySelectedProjectAreaId);
		Task<BaseChangeEntityResult> UpdateEntitySelectedProjectArea(UpdateEntitySelectedProjectAreaDto update);
		Task<BaseChangeEntityResult> DeleteEntitySelectedProjectArea(int entitySelectedProjectAreaId);
		Task DeleteEntitySelectedProjectArea(List<int> entitySelectedProjectAreasId);

        #endregion

        #region entity relations

		Task<FilterEntityRelationsDto> FilterEntityRelations(FilterEntityRelationsDto filter);
        Task<List<ComboDto>> GetEntityRelationsAsCombo(FilterEntityRelationsDto filter);
		Task<EntityRelationListDto?> GetSingleEntityRelationInformation(int entityRelationId);

        		Task<BaseChangeEntityResult> CreateEntityRelation(CreateEntityRelationDto create);
		Task<UpdateEntityRelationDto?> GetEntityRelationInformation(int entityRelationId);
		Task<BaseChangeEntityResult> UpdateEntityRelation(UpdateEntityRelationDto update);
		Task<BaseChangeEntityResult> DeleteEntityRelation(int entityRelationId);
		Task DeleteEntityRelation(List<int> entityRelationsId);

        #endregion

        #region entities

		Task<FilterEntitiesDto> FilterEntities(FilterEntitiesDto filter);
        Task<List<ComboDto>> GetEntitiesAsCombo(FilterEntitiesDto filter);
		Task<EntityListDto?> GetSingleEntityInformation(int entityId);

        		Task<ChangeEntityResult> CreateEntity(CreateEntityDto create);
		Task<UpdateEntityDto?> GetEntityInformation(int entityId);
		Task<ChangeEntityResult> UpdateEntity(UpdateEntityDto update);
		Task<BaseChangeEntityResult> DeleteEntity(int entityId);
		Task DeleteEntity(List<int> entitiesId);

        #endregion

        #endregion
    }
}
