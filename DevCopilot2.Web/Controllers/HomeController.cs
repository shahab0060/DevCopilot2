using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.SiteSettings;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DevCopilot2.Web.Controllers
{
    public class HomeController : BaseSiteController
    {
        #region constructor

        private readonly ISiteService _siteService;
        public HomeController(ISiteService siteService)
        {
            this._siteService = siteService;
        }

        #endregion

        #region index

        public IActionResult Index()
        => RedirectToAction("Index", "Home", new { area = "Admin" });

        #endregion

        #region set language

        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            try
            {
                return LocalRedirect(returnUrl);
            }
            catch
            {
                return LocalRedirect("/");
            }
        }

        #endregion

        #region not found

        [HttpGet("404")]
        public IActionResult NotFound()
       => View();

        #endregion

    }
}