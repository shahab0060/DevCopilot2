using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Users
{
    [PermissionChecker("UserManagement")]
    public class UserController : BaseAdminController<UserListDto>
    {

        #region constructor

        private readonly IRoleService _roleService;
private readonly IUserService _userService;
        public UserController(
                              IRoleService roleService,

                              IUserService userService )
        {
            this._roleService = roleService;
this._userService = userService;
        }

        #endregion

		#region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterUsersDto filter)
        {

            return View(await _userService.FilterUsers(filter));
        }

        #endregion

		#region detail

		[HttpGet]
		public async Task<IActionResult>Detail(long id)
		{
			UserListDto? userInformation = await _userService.GetSingleUserInformation(id);
			if (userInformation is null)return NotFound();
			await GetViewDatas();
            return View(userInformation);	
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
		public async Task<IActionResult> Create(CreateUserDto create)
		{

            create.RoleIds = create.RoleIds
                                                                             .Where(a=>a>0)
                                                                             .ToList();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _userService.CreateUser(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"کاربر مورد نظر با موفقیت اضافه شد";
					return RedirectToAction("Index", "User", new { Area = "Admin", });
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
			UpdateUserDto? userInformation = await _userService.GetUserInformation(id);
			if (userInformation is null) return NotFound();
			await GetViewDatas();
            return View(userInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateUserDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _userService.UpdateUser(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"کاربر مورد نظر با موفقیت ویرایش شد";
					return RedirectToAction("Index", "User", new { Area = "Admin", });
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
			BaseChangeEntityResult result = await _userService.DeleteUser(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"کاربر مورد نظر با موفقیت حذف شد";
						return RedirectToAction("Index", "User", new { Area = "Admin" });
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
				return RedirectToAction("Index", "User", new { Area = "Admin" });
			}
			await _userService.DeleteUser(ids);
			TempData[SuccessMessage] = $"کاربر ها مورد نظر با موفقیت حذف شد";
			return RedirectToAction("Index", "User", new { Area = "Admin" });
		}

		#endregion

        #region view datas

        async Task GetViewDatas()
        {

            await GetRolesViewData();
        }

        async Task GetRolesViewData()
        => ViewData["Roles"] = (await _roleService
        .GetRolesAsCombo(new FilterRolesDto()))
        .ToSelectListItem();

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterUsersDto filter)
        {
            List<UserListDto> result = (await _userService.FilterUsers(filter)).Users;
            string title = $"گزارش کاربر ها";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<UserListDto> excelExporter = new ExcelExporter<UserListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, "ردیف", 1, 3);
                ws = excelExporter.AddColumn(ws,"ادمین کل هست / نیست", 2, 3);
ws = excelExporter.AddColumn(ws,"نام", 3, 3);
ws = excelExporter.AddColumn(ws,"نام خانوادگی", 4, 3);
ws = excelExporter.AddColumn(ws,"شماره تلفن", 5, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsSuperAdmin.ConvertBoolToText(), 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FirstName??"-", 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LastName??"-", 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PhoneNumber, 5, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterUsersDto filter)
        {
            List<UserListDto> result = (await _userService.FilterUsers(filter)).Users;
            string title = "لیست کاربر ها";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader("ردیف");

            dataTable = dataTable.AddHeader("ادمین کل هست / نیست");

            dataTable = dataTable.AddHeader("نام");

            dataTable = dataTable.AddHeader("نام خانوادگی");

            dataTable = dataTable.AddHeader("شماره تلفن");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.IsSuperAdmin.ConvertBoolToText(),

item.FirstName??"-",

item.LastName??"-",

item.PhoneNumber );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
