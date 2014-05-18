using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Concrete;
using EazyWizy.Domain.Entities;
using EazyWizy.WebUI.Models;
using EDC = EazyWizy.Domain.Concrete;
using EazyWizy.WebUI.Utilities;

namespace EazyWizy.WebUI.Controllers.Cart
{
    public class CartController : Controller
    {
        IProductRepository repository;
        
        public CartController()
        {
            repository = new ProductRepository();
        }

        //public RedirectToRouteResult AddToCart(EDC.Cart cart, int productId, string returnUrl)
        //{
        //    Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
        //    if (product != null)
        //    {
        //        cart.AddItem(product, 1);
        //    }

        //    return RedirectToAction("Index", new { returnUrl });
        //}
        [HttpPost]
        public ActionResult AddToCart(EDC.Cart cart, int productId, int quantity)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, quantity);
                var result = new CartResponseViewModel()
                {
                    Message = Server.HtmlEncode(string.Format(Constants.ADD_ITEM_TO_CART_MSG, product.Name)),
                    CartTotal = cart.ComputeTotalValue().ToString("c"),
                    ItemCount = cart.Lines.Sum(c => c.Quantity)
                };
                return Json(result);
            }
            else
                return Json(new CartResponseViewModel()
                {
                    Message = Constants.PRODUCT_CURRENTLY_UNAVAILABLE,
                    CartTotal = cart.ComputeTotalValue().ToString("c"),
                    ItemCount = cart.Lines.Sum(c => c.Quantity)
                });
        }

        //public RedirectToRouteResult RemoveFromCart(EDC.Cart cart, int productId, string returnUrl)
        //{
        //    Product product = repository.Products
        //    .FirstOrDefault(p => p.ProductId == productId);
        //    if (product != null)
        //    {
        //        cart.RemoveLine(product);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}

        [HttpPost]
        public ActionResult RemoveFromCart(EDC.Cart cart, int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                if (cart.Lines.Select(x => x.Product.ProductId == productId).Count() > 0)
                {
                    cart.RemoveLine(product);
                    return Json(new CartResponseViewModel()
                    {
                        Message = string.Format(Constants.REMOVE_ITEM_FROM_CART_MSG, product.Name),
                        CartTotal = cart.ComputeTotalValue().ToString("c"),
                        ItemCount = cart.Lines.Sum(c => c.Quantity)
                    });
                }
            }
            return Json(new CartResponseViewModel()
            {
                Message = string.Format(Constants.NO_PRODUCT_TO_REMOVE_MSG, product.Name),
                CartTotal = cart.ComputeTotalValue().ToString("c"),
                ItemCount = cart.Lines.Sum(c => c.Quantity)
            });
        }

        public ViewResult Index(EDC.Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(EDC.Cart cart)
        {
            return PartialView(cart);
        }

        public PartialViewResult CartDetails(EDC.Cart cart)
        {
            return PartialView(cart);
        }

        public PartialViewResult CheckOutDetails(EDC.Cart cart)
        {
            return PartialView();
        }        
	}
}