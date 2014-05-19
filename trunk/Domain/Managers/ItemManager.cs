using Domain.Entites;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public class ItemManager
    {
        static readonly IDataBaseRepository repository = IocContainer.Resolve<IDataBaseRepository>();

        public static List<Category> GetCategories(long parentCategoryId)
        {
            try
            {
                return repository.GetCategory(parentCategoryId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static List<Category> GetAllCategories()
        {
            try
            {
                return repository.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void AddCategory(Category entity)
        {
            try
            {
                 repository.AddCategory(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void UpdateCategory(Category entity)
        {
            try
            {
                repository.UpdateCategory(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DeleteCategory(long id)
        {
            try
            {
                repository.RemoveCategory(id);
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
