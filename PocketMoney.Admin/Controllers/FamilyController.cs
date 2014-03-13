using System;
using System.Web.Mvc;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.FileSystem;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Network;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Admin.Controllers
{
    [Authorize]
    public class FamilyController : BaseController
    {
        #region Members & Ctors

        private IFamilyService _familyService;
        private IConnector _connector;

        public FamilyController(
            IFamilyService familyService,
            IConnector connector,
            ICurrentUserProvider currentUserProvider,
            IFileService fileService,
            IAuthorization authorization)
            : base(authorization, fileService, currentUserProvider)
        {
            _familyService = familyService;
            _connector = connector;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult Index()
        {
            return View(_currentUserProvider.GetCurrentUser());
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            return Json(
                _familyService.GetUsers(new FamilyRequest { Data = _currentUserProvider.GetCurrentUser().Family }),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserVK(string id)
        {
            return Json(
                _connector.GetAccount(new StringNetworkRequest { Data = id, Type = Model.NetworkType.VK }),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUser(byte roleId, string username, string email, bool send, Guid fileId)
        {
            return Json(null);
        }

        #endregion
    }
}
