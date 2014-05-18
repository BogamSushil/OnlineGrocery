using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly EazyWizyEntities dataSource = new EazyWizyEntities();

        public IQueryable<Product> Products
        {
            get { return dataSource.GetAllProduct().AsQueryable(); }
        }

        public IList<Product> GetAllProduct()
        {
            return dataSource.GetAllProduct().ToList<Product>();
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
