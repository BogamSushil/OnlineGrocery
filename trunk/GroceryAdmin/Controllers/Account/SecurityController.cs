using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Portal.Controllers.Account
{
    public class SecurityController : Controller
    {
        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult SessionExpired()
        {
            return View();
        }
	}
}