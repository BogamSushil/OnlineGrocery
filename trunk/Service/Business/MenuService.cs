using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{
    public class MenuService : BaseService
    {
        #region "Public methods"

        public List<Domain.TopMenu> GetAllTopMenu()
        {
            var data = this.DBContext.sp_GetTopMenu().ToList();
            return data.Select(TranslateMenu).ToList();
        }

        public List<Domain.LHSMenu> GetAllLhsMenu()
        {
            var data = this.DBContext.sp_Select_GetLHSMenu().ToList();
            return data.Select(TranslateLhsMenu).ToList();
        }

        public List<Domain.SubMenuBrand> GetAllSubMenu()
        {
            var data = this.DBContext.sp_Select_GetSubMenuBrand().ToList();
            return data.Select(TranslateSubMenuBrand).ToList();
        }

        #endregion

        #region Private / Helper Methods

        private Domain.TopMenu TranslateMenu(Data.EFData.TopMenu entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.TopMenu()
            {
                IdMenu = entity.IdMenu,
                Name = entity.Name

            };
        }

        private Domain.SubMenuBrand TranslateSubMenuBrand(Data.EFData.SubMenuBrand entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.SubMenuBrand()
            {
                IdBrand = entity.IdBrand,
                Name = entity.Name,
                Image = entity.Image
            };
        }

        private Domain.LHSMenu TranslateLhsMenu(Data.EFData.LHSMenu entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.LHSMenu()
            {
                IdLHSMenu = entity.IdLHSMenu,
                Name = entity.Name,
                Image = entity.Image

            };
        }

        #endregion
    }
}
