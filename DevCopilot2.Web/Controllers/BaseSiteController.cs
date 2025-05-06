using DevCopilot2.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace DevCopilot2.Web.Controllers
{
    public class BaseSiteController : Controller
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

        #endregion

    }
}
