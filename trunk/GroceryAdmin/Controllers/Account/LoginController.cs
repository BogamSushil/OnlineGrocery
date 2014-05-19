using Admin.Portal.Models;
using Admin.Portal.Security;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.Portal.Controllers.Account
{
    public class LoginController : BaseController
    {
        public ActionResult Login()
        {
            try
            {
                if (CheckSignInUser())
                {
                    if (null != User && (User.IsInRole("Admin") || User.IsInRole("Super Admin")))
                    {
                        return RedirectToAction("DashBoard", "AdminHome");
                    }
                    else
                    {
                        return RedirectToAction("AccessDenied", "Security");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return this.On_Error(ex);
            }
        }

        private bool CheckSignInUser()
        {
            return Request.Cookies[FormsAuthentication.FormsCookieName] != null ? true : false;
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var provider = (AdminMembershipProvider)Membership.Provider;
                    if (provider.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
                        var user = (AdminMembershipUser)provider.GetUser(model.UserName, true);

                        if (user.UserRoleName == "Admin" || user.UserRoleName == "Super Admin")
                        {
                            Session["admin"] = user.FirstName;
                            return RedirectToAction("DashBoard", "AdminHome");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Name address and/or Password is incorrect");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                Logger.Error(ex.Message, ex);
                return View(model);
            }
        }

        [HttpGet]

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult LoggClientSideErrors(string error)
        {
            Logger.Error(error);
            return new JsonResult
            {
                Data = new
                {
                    Status = "Error Added"
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
	}
}