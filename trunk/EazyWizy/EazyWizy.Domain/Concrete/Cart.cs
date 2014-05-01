using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class Cart:ICart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.
                            Where(p => p.Product.product_id == product.product_id)
                            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.product_id == product.product_id);
        }

        public decimal ComputeTotalValue()
        {
            return (decimal)lineCollection.Sum(e => e.Product.product_price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
