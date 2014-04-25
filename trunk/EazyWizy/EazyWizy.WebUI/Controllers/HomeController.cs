using System.Web.Mvc;
using EazyWizy.WebUI.Models;
using Service.Business;

namespace EazyWizy.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private MenuService _Manager;
        public MenuService Manager
        {
            get { return _Manager ?? (_Manager = new MenuService()); }
        }

        public ViewResult Index()
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
            return View(contentModel);
        }
	}
}