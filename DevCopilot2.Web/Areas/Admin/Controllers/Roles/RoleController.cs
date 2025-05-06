
using System;

using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Domain.DTOs.Permissions;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Roles
{
	[PermissionChecker("RoleManagement")]
    public class RoleController : BaseAdminController<RoleListDto>
    {

        #region constructor

        private readonly IPermissionService _permissionService;
private readonly IRoleService _roleService;
        public RoleController(
                              IPermissionService permissionService,

                              IRoleService roleService )
        {
            this._permissionService = permissionService;
this._roleService = roleService;
        }

        #endregion

		#region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterRolesDto filter)
        {

            return View(await _roleService.FilterRoles(filter));
        }

        #endregion

		#region detail

		[HttpGet]
		public async Task<IActionResult>Detail(long id)
		{
			RoleListDto? roleInformation = await _roleService.GetSingleRoleInformation(id);
			if (roleInformation is null)return NotFound();
			await GetViewDatas();
            return View(roleInformation);	
		}

		#endregion

		#region create

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            await GetViewDatas();

            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateRoleDto create)
		{

            create.PermissionIds = create.PermissionIds
                                                                             .Where(a=>a>0)
                                                                             .ToList();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _roleService.CreateRole(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"نقش مورد نظر با موفقیت اضافه شد";
					return RedirectToAction("Index", "Role", new { Area = "Admin", });
				}

				case BaseChangeEntityResult.NotFound:
				{
					TempData[ErrorMessage] = $"متاسفیم ! اجازه انجام این عملیات رو نداریم";
					return NotFound();
				}

				case BaseChangeEntityResult.Exists:
				{
					TempData[ErrorMessage] = $"همچین آیتمی از قبل در سایت وجود دارد";
					break;
				}
			}

            #endregion

            await GetViewDatas();
			return View(create);
		}

		#endregion

		#region update

		[HttpGet]
		public async Task<IActionResult> Update(long id)
		{
			UpdateRoleDto? roleInformation = await _roleService.GetRoleInformation(id);
			if (roleInformation is null) return NotFound();
			await GetViewDatas();
            return View(roleInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateRoleDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _roleService.UpdateRole(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"نقش مورد نظر با موفقیت ویرایش شد";
					return RedirectToAction("Index", "Role", new { Area = "Admin", });
				}

				case BaseChangeEntityResult.NotFound:
				{
					TempData[ErrorMessage] = $"متاسفیم ! اجازه انجام این عملیات رو نداریم";
					return NotFound();
				}

				case BaseChangeEntityResult.Exists:
				{
					TempData[ErrorMessage] = $"همچین آیتمی از قبل در سایت وجود دارد";
					break;
				}
			}

            #endregion

            await GetViewDatas();
			return View(update);
		}

		#endregion

		#region delete

		[HttpGet]
		public async Task<IActionResult> Delete(long id)
		{
			BaseChangeEntityResult result = await _roleService.DeleteRole(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"نقش مورد نظر با موفقیت حذف شد";
						return RedirectToAction("Index", "Role", new { Area = "Admin" });
					}
			}
			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> DeleteRange(List<long> ids)
		{
			if (!ids.Distinct().Any())
			{
				TempData[ErrorMessage] = "لطفا حداقل یک مورد را انتخاب کنید";
				return RedirectToAction("Index", "Role", new { Area = "Admin" });
			}
			await _roleService.DeleteRole(ids);
			TempData[SuccessMessage] = $"نقش ها مورد نظر با موفقیت حذف شد";
			return RedirectToAction("Index", "Role", new { Area = "Admin" });
		}

		#endregion

        #region view datas

        async Task GetViewDatas()
        {

            await GetPermissionsViewData();
        }

        async Task GetPermissionsViewData()
        => ViewData["Permissions"] = (await _permissionService
        .GetPermissionsAsCombo(new FilterPermissionsDto()))
        .ToSelectListItem();

        #endregion

       #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterRolesDto filter)
        {
            List<RoleListDto> result = (await _roleService.FilterRoles(filter)).Roles;
            string title = $"گزارش نقش ها";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<RoleListDto> excelExporter = new ExcelExporter<RoleListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, "ردیف", 1, 3);
                ws = excelExporter.AddColumn(ws,"عنوان", 2, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Title, 2, rowIndex);

                    rowIndex++;
                }
                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    wbook.SaveAs(stream);
                    stream.Position = 0;
                    return ReturnExcel(stream, title);
                }
            }
        }

        #endregion

        #region export pdf

        [HttpGet]
        public async Task<IActionResult> ExportPdf(FilterRolesDto filter)
        {
            List<RoleListDto> result = (await _roleService.FilterRoles(filter)).Roles;
            string title = "لیست نقش ها";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader("ردیف");

            dataTable = dataTable.AddHeader("عنوان");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.Title );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
