using System.Security.Claims;
using System.Security.Principal;

namespace DevCopilot2.Web.PresentationExtensions
{
    public static class IdentityExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Convert.ToInt32(data.Value);
            }

            return default(int);
            //return 3;
        }
        public static int? GetNullableCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Convert.ToInt32(data.Value);
            }
            return null;
        }

        public static int GetCurrentUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetCurrentUserId();
        }

        public static int? GetNullableCurrentUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetNullableCurrentUserId();
        }
    }
}
