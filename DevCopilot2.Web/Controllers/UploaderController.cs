using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevCopilot2.Web.Controllers
{
    public class UploaderController : BaseSiteController
    {

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload, string CKEditorFuncName, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            if (!upload.IsImage())
            {
                var notImageMessage = "لطفا یک تصویر انتخاب کنید";
                var notImage = JsonConvert.DeserializeObject("{'uploaded':0, 'error': {'message': \" " + notImageMessage + " \"}}");
                return Json(notImage);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            string imageName =
                await upload.UploadImage(PathExtension.OriginalDocumentImageServer,
                fileName, 0, fileName) ?? "";
            return Json(new
            {
                uploaded = true,
                url = $"{PathExtension.OriginalDocumentImage}{imageName}"
            });
        }
    }
}