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

namespace EazyWizy.WebUI.Controllers.Cart
{
    public class CartController : Controller
    {
        IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(EDC.Cart cart, int productId, int quantity, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.product_id == productId);
            if (product != null)
            {
                cart.AddItem(product, quantity);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(EDC.Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.product_id == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
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
	}
}