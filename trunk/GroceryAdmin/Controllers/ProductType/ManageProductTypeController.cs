using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Business;

namespace Admin.Portal.Controllers.ProductType
{
    public class ManageProductTypeController : BaseController
    {
        #region "Properties"

        private ProductCategoryService manager;
        private ProductCategoryService Manager
        {
            get { return manager == null ? manager = new ProductCategoryService() : manager; }
        }

       

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
                var data = Manager.GetAll();
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Data.Add("AjaxError", true);
                return this.On_Error(ex);
            }
        }

       

        [AcceptVerbs(HttpVerbs.Post)]
      
        public ActionResult CreateCategory([DataSourceRequest] DataSourceRequest request, Domain.ProductCategory entity)
        {
            try
            {
                var errors = new Dictionary<string, string>();
                if (null != entity && ModelState.IsValid)
                {
                    Manager.Create(entity);                    
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
        public ActionResult UpdateCategory([DataSourceRequest] DataSourceRequest request, Domain.ProductCategory entity)
        {
            try
            {
               
                var errors = new Dictionary<string, string>();
                if (null != entity && ModelState.IsValid)
                {
                    Manager.Update(entity);
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
    
        public ActionResult DeleteCategory([DataSourceRequest] DataSourceRequest request, string Id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Id)  && ModelState.IsValid)
                {
                    Manager.DeleteProductCategory(Id);
                
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