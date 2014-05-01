using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EazyWizy.Domain.Concrete;

namespace EazyWizy.WebUI.Binder
{
    public class CartModelBinder : IModelBinder
    {
        private const string _sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)controllerContext.HttpContext.Session[_sessionKey];
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[_sessionKey] = cart;
            }
            return cart;
        }
    }
}