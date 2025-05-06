using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.API.PresentationExtensions;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Api.Controllers.Base;
using DevCopilot2.Api;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using DevCopilot2.Core.Exporters;

namespace DevCopilot2.API.Controllers.Projects
{
	//[PermissionChecker("ProjectEnumPropertyManagement")]
    public class ProjectEnumPropertyController : BaseApiController
    {

        #region constructor

        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public ProjectEnumPropertyController(
                           IProjectService projectService,
                           ISiteService siteService 
                                      )
        {
            this._projectService = projectService;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet("list")]
        public async Task<IActionResult> Index([FromQuery]FilterProjectEnumPropertiesDto filter)
        {
            return Ok(await _projectService.FilterProjectEnumProperties(filter));
        }

        #endregion

        #region detail

        [HttpGet("{id}")]
		public async Task<IActionResult>Detail(int id)
		{
			ProjectEnumPropertyListDto? projectEnumPropertyInformation = await _projectService.GetSingleProjectEnumPropertyInformation(id);
			if (projectEnumPropertyInformation is null)return NotFound();
            return Ok(projectEnumPropertyInformation);	
		}

		#endregion

        #region create

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectEnumPropertyDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			    return ReturnErrors(ModelState);
			BaseChangeEntityResult result = await _projectService.CreateProjectEnumProperty(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					string message = $"ProjectEnumProperty create";
					return Ok(message);
				}

				case BaseChangeEntityResult.NotFound:
				{
					string message = $"Invalid Request.";
					return NotFound(message);
				}

				case BaseChangeEntityResult.Exists:
				{
					string message = $"Item Exists.";
					return ReturnError(message);
				}
			}

            #endregion

			return NotFound();
        }

		#endregion

        #region update

		[HttpPut]
		public async Task<IActionResult> Update(UpdateProjectEnumPropertyDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			    return ReturnErrors(ModelState);
			BaseChangeEntityResult result = await _projectService.UpdateProjectEnumProperty(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					string message = $"ProjectEnumProperty update";
					return Ok(message);
				}

				case BaseChangeEntityResult.NotFound:
				{
					string message = $"Invalid Request.";
					return NotFound(message);
				}

				case BaseChangeEntityResult.Exists:
				{
					string message = $"Item Exists.";
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
			BaseChangeEntityResult result = await _projectService.DeleteProjectEnumProperty(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						string message = $"ProjectEnumProperty Deleted Successfully.";
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
			await _projectService.DeleteProjectEnumProperty(ids);
			string message = $"ProjectEnumProperties Deleted Successfully.";
			return Ok(message);
		}

		#endregion

    }
}
