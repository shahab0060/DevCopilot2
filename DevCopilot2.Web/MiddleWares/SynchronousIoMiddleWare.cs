using Microsoft.AspNetCore.Http.Features;

namespace DevCopilot2.Web.MiddleWares
{
    public class SynchronousIOMiddleware
    {
        private readonly RequestDelegate _next;

        public SynchronousIOMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Path.Value) && context.Request.Path.Value.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var syncIOFeature = context.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                }
            }

            await _next(context);
        }
    }

}
