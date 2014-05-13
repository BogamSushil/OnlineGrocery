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
        static IDataBaseRepository repository = IocContainer.Resolve<IDataBaseRepository>();

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
    }
}
