using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.Enums.Common;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http.Extensions;

namespace DevCopilot2.Web.PresentationExtensions
{
    public static class HttpExtensions
    {
        public static string GetUserIp(this HttpContext httpContext) => httpContext.Connection.RemoteIpAddress!.ToString();

        public static string GetCurrentPageUrl(this HttpContext context)
        => context.Request.Path;
        public static string GetCurrentDomain(this HttpContext context)
       => (context.Request.IsHttps ? "https://" : "http://") + context.Request.Host.Value;
        public static string GetCurrentHttpsDomain(this HttpContext context)
            => "https://" + context.Request.Host.Value;

        public static string GetCurrentPageFullAddress(this HttpContext context)
            => $"{context.GetCurrentHttpsDomain()}{context.GetCurrentPageUrl()}";
    }
}
