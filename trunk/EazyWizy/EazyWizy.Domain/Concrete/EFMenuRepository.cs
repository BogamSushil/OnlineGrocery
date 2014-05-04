using System.Linq;
using System.Collections.Generic;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class EFMenuRepository : IMenuRepository    
    {
        private EazyWizyEntities ewEntities = new EazyWizyEntities();        
        IEnumerable<TopMenu> IMenuRepository.TopMenu
        {
            get {
               return ewEntities.sp_Select_GetMenu();
            }
        }
        IList<LHSMenu> IMenuRepository.LHSMenu
        {
            get {
                return ewEntities.sp_Select_GetLHSMenu().ToList();
            }
        }
        IList<LHSSubMenuBrand> IMenuRepository.LHSSubMenuBrand
        {
            get {
                return ewEntities.sp_Select_GetSubMenuBrand().ToList();
            }
        }
        IList<LHSBrandFilter> IMenuRepository.LHSBrandFilter
        {
            get {
                return ewEntities.sp_Select_GetSubMenuBrandFilter().ToList();
            }
        }
    }
}
