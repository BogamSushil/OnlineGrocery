using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EazyWizy.Domain.Concrete;

namespace EazyWizy.WebUI.Models
{
    public class CartResponseViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int ItemCount { get; set; }
    }
}