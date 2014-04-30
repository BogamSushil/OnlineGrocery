using System.Collections.Generic;
using System.Linq;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Abstract
{
    public interface IMenuRepository
    {
        IEnumerable<TopMenu> TopMenu { get; }

        IList<LHSMenu> LHSMenu { get; }

        IList<SubMenu> LHSSubMenuBrand { get; }
    }
}
