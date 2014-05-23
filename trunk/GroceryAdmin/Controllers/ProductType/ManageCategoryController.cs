using Domain.Entites;
using Domain.Managers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Portal.Controllers.ProductType
{
    public class ManageCategoryController : BaseController
    {
        #region "Properties"

       

       

        #endregion

        [HttpGet]        
        public ActionResult Manage()
        {
            try
            {              
                ViewData["Admin"] = GetUserName();
                return View();
            }
            catch (Exception ex)
            {
                return this.On_Error(ex);
            }
        }
        [Authorize]
        
        public ActionResult GetCategories([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var data = ItemManager.GetAllCategories();
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }

       

        [AcceptVerbs(HttpVerbs.Post)]
      
        public ActionResult CreateCategory([DataSourceRequest] DataSourceRequest request, Category entity)
        {
            try
            {
                var errors = new Dictionary<string, string>();
                if (null != entity && ModelState.IsValid)
                {
                    ItemManager.AddCategory(entity);                    
                }
                else
                {
                    foreach (var item in errors)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return Json(new[] { entity }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCategory([DataSourceRequest] DataSourceRequest request, Category entity)
        {
            try
            {
               
                var errors = new Dictionary<string, string>();
                if (null != entity && ModelState.IsValid)
                {
                    ItemManager.UpdateCategory(entity);
                }
                else
                {
                    foreach (var item in errors)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return Json(new[] { entity }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteCategory([DataSourceRequest] DataSourceRequest request, long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemManager.DeleteCategory(Id);
                
                }
                return Json(new[] { Id }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ActiveDeactiveCategory([DataSourceRequest] DataSourceRequest request, long Id, bool status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemManager.ActiveDeactiveCategory(Id, status);

                }
                return Json(new[] { Id }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }
     

	}
}