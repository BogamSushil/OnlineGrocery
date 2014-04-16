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
        private ITopMenuRepository topMenuRepository;
        public HomeController()
        {
            //this.topMenuRepository = new EFTopMenuRepository();
        }
        public HomeController(ITopMenuRepository _topMenuRepository)
        {
            //this.topMenuRepository = _topMenuRepository;
        }

        public ViewResult Index()
        {
            //ContentModel contentModel = new ContentModel();
            //contentModel.TopMenu = this.topMenuRepository.TopMenu;
            return View();
        }
	}
}