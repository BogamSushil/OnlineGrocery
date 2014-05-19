using Admin.Portal.Security;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected AdminMembershipUser GetLoggedInUser()
        {
            var provider = (AdminMembershipProvider)Membership.Provider;
            var user = (AdminMembershipUser)provider.GetUser(User.Identity.Name, true);
            return user;
        }

        protected string GetUserName()
        {
            var provider = (AdminMembershipProvider)Membership.Provider;
            if (null != provider)
            {
                var user = (AdminMembershipUser)provider.GetUser(User.Identity.Name, true);
                if (null != user)
                {
                    ViewData["User"] = user.FirstName;
                    return user.FirstName;
                }
            }
            return string.Empty;
        }
        protected ActionResult On_Error(Exception ex)
        {
            Logger.Error(ex.Message, ex);

            var error = ex as AuthenticationFailedException;
            if (null != error)
            {
                ViewBag.Error = ex.Message;
            }
            else
            {
                ViewBag.Error = "An error occurred while processing your request.";
            }
            if (ex.Data.Contains("AjaxError"))
            {
                throw ex;
            }
            else
            {
                return View("Error");
            }
        }
	}
}