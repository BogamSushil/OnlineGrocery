using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EFData;
using Domain.Interfaces;

namespace Data.Repository
{
    public class EasyWizyRepository : IDataBaseRepository
    {
        public List<Category> GetCategory(long parentCategoryId)
        {
            using(EazyWizyEntities entities = new EazyWizyEntities())
            {
                var result = entities.GetCategories(parentCategoryId);
                return (from item in result select new Category() { CategoryId = item.CategoryId, ImageName = item.ImageName, Description = item.Description, IsActive = item.IsActive.HasValue ? item.IsActive.Value : false, Name = item.Name, ParentCategoryId = parentCategoryId }).ToList();
            }
        }
    }
}
