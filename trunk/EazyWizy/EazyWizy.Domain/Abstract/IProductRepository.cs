using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Abstract
{
  public  interface IProductRepository
  {
      IEnumerable<Product> Products { get; }
      IList<Product> GetAllProduct();
      IList<Product> GetProductByCategory(int categoryId);
      IList<Product> GetProductByBrand(int brandId);
  }
}
