using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Portal.Controllers
{
    public class AdminHomeController : BaseController
    {
       
        public ActionResult DashBoard()
        {
            ViewData["Admin"] = GetUserName();
            return View();
        }
	}
}