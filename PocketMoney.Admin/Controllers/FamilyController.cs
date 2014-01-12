using System.Web.Mvc;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Network;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Admin.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
        #region Members & Ctors

        private IFamilyService _familyService;
        private ICurrentUserProvider _currentUserProvider;
        private IConnector _connector;

        public FamilyController(
            IFamilyService familyService,
            IConnector connector,
            ICurrentUserProvider currentUserProvider)
        {
            _familyService = familyService;
            _connector = connector;
            _currentUserProvider = currentUserProvider;
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

        [HttpGet]
        public JsonResult SearchUsersVK(string q)
        {
            return Json(
                _connector.SearchAccount(new StringNetworkRequest { Data = q, Type = Model.NetworkType.VK }),
                JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
