using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Concrete;
using EazyWizy.Domain.Entities;
using EazyWizy.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EazyWizy.WebUI.Controllers.Menu
{
    public class LHSMenuController : Controller
    {
 
        private IMenuRepository topMenuRepository;
        public LHSMenuController()
        {
            topMenuRepository = new EFMenuRepository();
        }  
	}
}