﻿using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Concrete;
using EazyWizy.Domain.Entities;
using EazyWizy.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EazyWizy.WebUI.Controllers.Content
{
    public class ContentController : Controller
    {
        private IMenuRepository menuRepository;
        public ContentController()
        {
            this.menuRepository = new EFMenuRepository();
        }
        public ActionResult ProductDisplay()
        {
            ContentModel contentModel = new ContentModel();
            contentModel.topMenu = this.menuRepository.TopMenu;
            
            contentModel.lhsAllMenu = new LHSAllMenu()
            {
                lhsMenu = this.menuRepository.LHSMenu,
                lhsSubMenuBrand = this.menuRepository.LHSSubMenuBrand
            };
            contentModel.Products = (new ProductRepository()).GetAllProduct();
            return View("ProductDisplay", contentModel);
        }
	}

    
}