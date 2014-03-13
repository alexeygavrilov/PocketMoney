using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.FileSystem;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;

namespace PocketMoney.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ICurrentUserProvider currentUserProvider,
            IFileService fileService,
            IAuthorization authorization)
            : base(authorization, fileService, currentUserProvider)
        {
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Download(string id)
        {
            Guid fileId;
            if (id.TryParseGuidFromBase32Url(out fileId))
            {
                try
                {
                    var file = _fileService.Retrieve(new FileId(fileId));
                    return new FileContentResult(file.Content, file.FileFormat.MimeTypes);
                }
                catch (System.IO.FileNotFoundException) { }
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            string fileUrl = string.Empty;
            return Json(fileUrl);
        }
    }
}
