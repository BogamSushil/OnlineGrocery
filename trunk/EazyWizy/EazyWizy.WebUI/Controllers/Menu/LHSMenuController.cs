using System.Web.Mvc;

namespace EazyWizy.WebUI.Controllers.Menu
{
    public class LHSMenuController : Controller
    {
        //public JsonResult GetMenu()
        //{
        //    IEnumerable<LHSMenu> _lhsmenu = this.topMenuRepository.LHSMenu;
        //    var result = _lhsmenu.Select(menu => new LMenu()
        //    {
        //        imageUrl = "Content/Images/" + menu.Image,
        //        text = menu.Name,
        //        //items = this.topMenuRepository.LHSMenu.Select(submenu => new LMenu() { imageUrl = "Content/Images/" + submenu.Image, text = submenu.Name })
        //    }).ToList();
        //    return new JsonResult() { Data = result, ContentEncoding = Encoding.UTF8, ContentType = "application/json", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
	}
}