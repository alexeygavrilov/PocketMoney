using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Members & Ctors

        private IFamilyService _familyService;
        private ICurrentUserProvider _currentUserProvider;

        public AccountController(
            IFamilyService familyService,
            ICurrentUserProvider currentUserProvider)
        {
            _familyService = familyService;
            _currentUserProvider = currentUserProvider;
        }

        #endregion

        #region Membership
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = _familyService.Login(model);
                if (result.Success)
                {
                    _currentUserProvider.AddCurrentUser(result.Data, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _currentUserProvider.RemoveCurrentUser();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = _familyService.RegisterUser(model);
                if (result.Success)
                {
                    return RedirectToAction("Confirm", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Confirm(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var result = _familyService.ConfirmUser(new ConfirmUserRequest { ConfirmCode = code });
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(ConfirmUserRequest model)
        {

            if (ModelState.IsValid)
            {
                var result = _familyService.ConfirmUser(model);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(model);
        }

        #endregion

        #region Family

        public ActionResult Family()
        {
            var users = _familyService.GetUsers(new FamilyRequest { Data = _currentUserProvider.GetCurrentUser().Family });
            if (!users.Success)
                throw new Exception(users.Message);
            else
                return View(users);
        }

        #endregion

        #region Private Methods

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
