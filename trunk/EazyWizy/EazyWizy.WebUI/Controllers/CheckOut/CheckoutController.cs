using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EazyWizy.Domain.Entities;

namespace EazyWizy.WebUI.Controllers.CheckOut
{
    public class CheckoutController : Controller
    {
        public PartialViewResult SignIn()
        {
            return PartialView();
        }
    }
}
