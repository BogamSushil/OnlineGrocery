using EazyWizy.WebUI.Models;
using System.Web.Mvc;
using Service.Business;

namespace EazyWizy.WebUI.Controllers.Content
{
    public class ContentController : Controller
    {
        private MenuService _mManager;
        public MenuService Manager
        {
            get { return _mManager ?? (_mManager = new MenuService()); }
        }
        public ActionResult ProductDisplay()
        {
            var contentModel = new ContentModel
            {
                topMenu = Manager.GetAllTopMenu(),
                lhsAllMenu = new LHSAllMenu()
                {
                    lhsMenu = Manager.GetAllLhsMenu(),
                    lhsSubMenuBrand = Manager.GetAllSubMenu()
                }
            };
            return View("ProductDisplay", contentModel);
        }
	}
}