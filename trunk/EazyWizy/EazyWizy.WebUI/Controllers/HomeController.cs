using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EazyWizy.Domain.Abstract;
using EazyWizy.Domain;
using EazyWizy.Domain.Concrete;
using EazyWizy.WebUI.Models;

namespace EazyWizy.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IMenuRepository menuRepository;
        public HomeController()
        {
            this.menuRepository = new EFMenuRepository();
        }

        public ViewResult Index()
        {
            ContentModel contentModel = new ContentModel();
            contentModel.topMenu = this.menuRepository.TopMenu;
            contentModel.lhsAllMenu = new LHSAllMenu()
            {
                lhsMenu = this.menuRepository.LHSMenu,
                lhsSubMenuBrand = this.menuRepository.LHSSubMenuBrand
            }; 
            return View(contentModel);
        }
	}
}