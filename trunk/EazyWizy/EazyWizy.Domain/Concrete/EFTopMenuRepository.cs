using System.Linq;
using System.Collections.Generic;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Concrete
{
    public class EFTopMenuRepository : ITopMenuRepository    
    {
        private EazyWizyEntities ewEntities = new EazyWizyEntities();        
        IEnumerable<Entities.TopMenu> ITopMenuRepository.TopMenu
        {
            get {
               return ewEntities.sp_Select_GetMenu();
            }
        }
    }
}
