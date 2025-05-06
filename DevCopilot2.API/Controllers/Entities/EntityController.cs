using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.API.PresentationExtensions;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Api.Controllers.Base;
using DevCopilot2.Api;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using DevCopilot2.Core.Exporters;

namespace DevCopilot2.API.Controllers.Entities
{
	//[PermissionChecker("EntityManagement")]
    public class EntityController : BaseApiController
    {

        #region constructor

        private readonly IEntityService _entityService;
        private readonly IProjectService _projectService;
        private readonly IPropertyService _propertyService;
        private readonly ISiteService _siteService;
        public EntityController(
                           IEntityService entityService,
                           IProjectService projectService,
                           IPropertyService propertyService,
                           ISiteService siteService 
                                      )
        {
            this._entityService = entityService;
            this._projectService = projectService;
            this._propertyService = propertyService;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet("list")]
        public async Task<IActionResult> Index([FromQuery]FilterEntitiesDto filter)
        {
            return Ok(await _entityService.FilterEntities(filter));
        }

        #endregion

        #region detail

        [HttpGet("{id}")]
		public async Task<IActionResult>Detail(int id)
		{
			EntityListDto? entityInformation = await _entityService.GetSingleEntityInformation(id);
			if (entityInformation is null)return NotFound();
            return Ok(entityInformation);	
		}

		#endregion

        #region create

		[HttpPost]
		public async Task<IActionResult> Create(CreateEntityDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			    return ReturnErrors(ModelState);
			ChangeEntityResult result = await _entityService.CreateEntity(create);

            #region handling different types

            switch (result)
			{

				case ChangeEntityResult.Success:
                    {
                        string message = $"Entity Created Successfully.";
                        return Ok(message);
                    }

				case ChangeEntityResult.NotFound:
                    {
                        string message = $"Invalid Request.";
                        return NotFound(message);
                    }

				case ChangeEntityResult.SingularNameExists:
                    {
                        string message = $"A Entity Exists With This SingularName {create.SingularName}";
                        return ReturnError(message);
                    }

				case ChangeEntityResult.PluralNameExists:
                    {
                        string message = $"A Entity Exists With This PluralName {create.PluralName}";
                        return ReturnError(message);
                    }

			}

            #endregion

			return NotFound();
        }

		#endregion

        #region update

		[HttpPut]
		public async Task<IActionResult> Update(UpdateEntityDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			    return ReturnErrors(ModelState);
			ChangeEntityResult result = await _entityService.UpdateEntity(update);

            #region handling different types

            switch (result)
			{

				case ChangeEntityResult.Success:
                    {
                        string message = $"Entity Updated Successfully.";
                        return Ok(message);
                    }

				case ChangeEntityResult.NotFound:
                    {
                        string message = $"Invalid Request.";
                        return NotFound(message);
                    }

				case ChangeEntityResult.SingularNameExists:
                    {
                        string message = $"A Entity Exists With This SingularName {update.SingularName}";
                        return ReturnError(message);
                    }

				case ChangeEntityResult.PluralNameExists:
                    {
                        string message = $"A Entity Exists With This PluralName {update.PluralName}";
                        return ReturnError(message);
                    }

			}

            #endregion

			return NotFound();
		}

		#endregion

        #region delete

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			BaseChangeEntityResult result = await _entityService.DeleteEntity(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						string message = $"Entity Deleted Successfully.";
						return Ok(message);
					}
			}
			return NotFound();
		}

		[HttpDelete("range")]
		public async Task<IActionResult> DeleteRange(List<int> ids)
		{
			if (!ids.Distinct().Any())
			{
				string deleteMessage = $"Please AtLeast Choose One Item.";
				return ReturnError(deleteMessage);
			}
			await _entityService.DeleteEntity(ids);
			string message = $"Entities Deleted Successfully.";
			return Ok(message);
		}

		#endregion

    }
}
