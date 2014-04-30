using System.Linq;
using System.Collections.Generic;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class EFMenuRepository : IMenuRepository    
    {
        private EazyWizyEntities ewEntities = new EazyWizyEntities();        
        IEnumerable<Entities.TopMenu> IMenuRepository.TopMenu
        {
            get {
                return ewEntities.sp_GetTopMenu();
            }
        }
        IList<Entities.LHSMenu> IMenuRepository.LHSMenu
        {
            get
            {
                return ewEntities.sp_Select_GetLHSMenu().ToList();
            }
        }
        IList<Entities.SubMenu> IMenuRepository.LHSSubMenuBrand
        {
            get
            {
                return ewEntities.sp_Select_GetSubMenuBrand().ToList();
            }
        }
    }
}
