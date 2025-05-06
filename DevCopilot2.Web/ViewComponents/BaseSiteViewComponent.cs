using DevCopilot2.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevCopilot2.Web.ViewComponents
{
    #region meta tags and title

    public class SiteMetaTagsAndTitleViewComponent : ViewComponent
    {
        #region constructor

        private readonly ISiteService _siteService;
        public SiteMetaTagsAndTitleViewComponent(ISiteService siteService)
        {
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
            => View("SiteMetaTagsAndTitle",
                await _siteService
                .GetSingleSiteSettingInformation(1));
    }

    #endregion

    #region meta tags and title

    public class SiteHeaderScriptsViewComponent : ViewComponent
    {
        #region constructor

        private readonly ISiteService _siteService;
        public SiteHeaderScriptsViewComponent(ISiteService siteService)
        {
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
            => View("SiteHeaderScripts",
                await _siteService
                .GetSingleSiteSettingInformation(1));
    }

    #endregion

}
