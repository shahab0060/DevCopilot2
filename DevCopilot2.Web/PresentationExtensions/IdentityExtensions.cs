using System.Security.Claims;
using System.Security.Principal;

namespace DevCopilot2.Web.PresentationExtensions
{
    public static class IdentityExtensions
    {
        public static long GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Convert.ToInt64(data.Value);
            }

            return default(long);
            //return 3;
        }
        public static long? GetNullableCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Convert.ToInt64(data.Value);
            }
            return null;
        }

        public static long GetCurrentUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetCurrentUserId();
        }

        public static long? GetNullableCurrentUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetNullableCurrentUserId();
        }
    }
}
