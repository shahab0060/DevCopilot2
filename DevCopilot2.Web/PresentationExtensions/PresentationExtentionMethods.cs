using DevCopilot2.Domain.DTOs.Common;
using System.Security.Claims;

namespace DevCopilot2.Web.PresentationExtensions
{
    public static class PresentationExtentionMethods
    {
        public static BaseUserIdentityInformationDto GetBaseUserIdentityInformation(this ClaimsPrincipal claimsPrincipal, HttpContext context)
           => new BaseUserIdentityInformationDto()
           {
               UserIp = context.GetUserIp(),
               UserId = claimsPrincipal.GetNullableCurrentUserId(),
           };

        public static BaseUserIdentityInformationDto GetBaseUserIdentityInformation(this ClaimsPrincipal claimsPrincipal,
            HttpContext context, Guid userIdentityKey)
          => new BaseUserIdentityInformationDto()
          {
              UserIp = context.GetUserIp(),
              UserId = claimsPrincipal.GetNullableCurrentUserId(),
              UserIdentityKey = userIdentityKey
          };

        //public static string RenderPartialView(Controller.Controller controller,string viewname,object? model)
        //{

        //    controller.ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewname);
        //        var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
        //        viewResult.View.Render(viewContext,sw);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
    }
}
