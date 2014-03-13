using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.FileSystem;

namespace PocketMoney.Admin.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IAuthorization _authorization;
        protected IFileService _fileService;
        protected ICurrentUserProvider _currentUserProvider;

        public BaseController(IAuthorization authorization, IFileService fileService, ICurrentUserProvider currentUserProvider)
        {
            _authorization = authorization;
            _fileService = fileService;
            _currentUserProvider = currentUserProvider;
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            IFile stored = _fileService.Store(FileFactory.FromHttpPostedImage(file), currentUser.Family, _authorization.Create(currentUser.Family, ObjectPermissions.FullControl).Build(), currentUser);
            return Json(new { fileId = stored.Id, fileUrl = stored.DownloadURL() });
        }

    }
}
