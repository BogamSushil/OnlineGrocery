using System.Linq;
using System.Collections.Generic;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class ProductRepository : IProductRepository    
    {
        private readonly EazyWizyEntities _dataSource = new EazyWizyEntities();

        public IList<Product> GetAllProduct()
        {
            return _dataSource.GetAllProduct(null).ToList();
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            return _dataSource.GetAllProduct(categoryId).ToList();
        }

        public IList<Product> GetProductByBrand(int brandId)
        {
            throw new System.NotImplementedException();
        }
    }
}
