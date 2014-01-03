using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IFamilyService _familyService;

        public AccountController(
            IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
        //    {
        //        return RedirectToLocal(returnUrl);
        //    }

        //    // Появление этого сообщения означает наличие ошибки; повторное отображение формы
        //    ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

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
    }
}
