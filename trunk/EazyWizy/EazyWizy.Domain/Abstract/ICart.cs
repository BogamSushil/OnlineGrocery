using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Abstract
{
    public interface ICart
    {
        void AddItem(Product product, int quantity);
        void RemoveLine(Product product);
        decimal ComputeTotalValue();
        void Clear();
    }
}
