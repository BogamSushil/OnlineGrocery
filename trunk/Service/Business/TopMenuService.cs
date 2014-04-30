using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{

    #region "Public methods"

    public class TopMenuService : BaseService
    {
        public List<Domain.TopMenu> GetTopMenu()
        {
            var data = this.DBContext.sp_GetTopMenu();
            return data.Select(topMenu => this.Translate(topMenu)).ToList();
        }


        #endregion

        #region Private / Helper Methods

        private Domain.TopMenu Translate(Data.EFData.TopMenu entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.TopMenu
            {
               IdMenu = entity.IdMenu,
               Name = entity.Name
            };
        }

        #endregion

    }
}
