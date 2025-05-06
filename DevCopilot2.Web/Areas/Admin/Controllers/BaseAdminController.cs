using DevCopilot2.Web.Http;
using DevCopilot2.DataLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DevCopilot2.Core.Exporters;

namespace DevCopilot2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BaseAdminController<T> : Controller where T : class
    {
        #region Messages

        protected string SuccessMessage = "SuccessMessage";
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";

        #endregion

        #region Methods

        #region Get Model State Validation Error Message Json

        protected IActionResult GetModelStateValidationErrorMessageJson(ModelStateDictionary modelState)
        {
            var firstValue = modelState.Values.FirstOrDefault();
            string? firstErrorMessage = null;
            if (firstValue != null)
            {
                var firstError = firstValue.Errors.FirstOrDefault();
                if (firstError != null)
                    firstErrorMessage = firstError.ErrorMessage;
            }
            if (firstErrorMessage != null)
                return JsonResponseStatusType.Danger.SendStatus(firstErrorMessage, null);
            return JsonResponseStatusType.Danger.SendStatus("لطفا مقادیر را به درستی وارد کنید.", null);
            //modelState.Values
            //    .FirstOrDefault()
            //    !.Errors
            //    .FirstOrDefault()
            //    !.ErrorMessage, null
        }
        #endregion

        protected IActionResult ReturnExcel(List<T> data, string title)
        {
            ExcelExporter<T> excelExporter = new ExcelExporter<T>();
            var stream = excelExporter.Export(data, title);
            return ReturnExcel(stream, title);
        }

        protected IActionResult ReturnExcel(MemoryStream stream, string title)
        {
            return File(stream.ToArray(), "application/octet-stream", $"{title}.xlsx");
        }

        protected IActionResult ReturnPdf(List<T> data, string title)
        {
            //var pdf = new PdfExporter<T>();
            //var stream = pdf.Export(data, title);
            var stream = new MemoryStream();
            return File(stream.ToArray(), "application/octet-stream", $"{title}.pdf");
        }

        protected IActionResult ReturnPdf(MemoryStream stream, string title)
        {
            //return new FileStreamResult(stream, "application/ms-pdf")
            //{
            //    FileDownloadName = $"{title}.pdf"
            //};
            return File(stream.ToArray(), "application/octet-stream", $"{title}.pdf");
        }

        #endregion

    }

    #region permission checker

    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private DevCopilot2DbContext _context;
        private string _permissionName = null;

        public PermissionCheckerAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }
            
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            _context = (DevCopilot2DbContext)context.HttpContext.RequestServices.GetService(typeof(DevCopilot2DbContext));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var data = context.HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                long userId = Convert.ToInt64(data.Value);
                if (_context.Users.Any(a => a.Id == userId && a.IsSuperAdmin)) return;
                if (string.IsNullOrEmpty(_permissionName))
                {
                    if (_context
                    .Users
                    .Where(a => a.Id == userId)
                    .Include(a => a.UserSelectedRoles)
                    .ThenInclude(a => a.Role)
                    .ThenInclude(a => a.RoleSelectedPermissions)
                    .ThenInclude(a => a.Permission)
                    .Any(p => p.UserSelectedRoles.Any())) return;
                    else context.Result = new RedirectResult("/");

                }
                else
                if (_context
                    .Users
                    .Where(a => a.Id == userId)
                    .Include(a => a.UserSelectedRoles)
                    .ThenInclude(a => a.Role)
                    .ThenInclude(a => a.RoleSelectedPermissions)
                    .ThenInclude(a => a.Permission)
                    .Any(p => p.UserSelectedRoles.Any(r => r.Role.RoleSelectedPermissions.Any(d => d.Permission.KeyName == _permissionName)))) return;
                context.Result = new RedirectResult("/");
            }
            else
            {
                context.Result = new RedirectResult("/");
            }
        }

        #endregion

    }
}
