using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;
using Domain.Interfaces;

namespace Domain.Managers
{
    class ProductManager
    {
        static readonly IDataBaseRepository repository = IocContainer.Resolve<IDataBaseRepository>();

        public static List<Product> GetProducts(long categoryId)
        {
            try
            {
                return repository.GetProducts(categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<Product> GetAllProducts()
        {
            try
            {
                return repository.GetAllProdcts();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void AddProduct(Product entity)
        {
            try
            {
                repository.AddProduct(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void UpdateProduct(Product entity)
        {
            try
            {
                repository.UpdateProduct(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DeleteProduct(long id)
        {
            try
            {
                repository.RemoveProduct(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void ActiveDeactiveCategory(long id, bool status)
        {
            try
            {
                repository.ActiveDeactiveCategory(id, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
