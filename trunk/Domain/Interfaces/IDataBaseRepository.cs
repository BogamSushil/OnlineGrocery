using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataBaseRepository
    {
        List<Category> GetCategory(long parentCategoryId);
    }
}
