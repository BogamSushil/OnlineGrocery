using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace EazyWizy.WebUI.Models
{
    public class ContentModel
    {
        public IEnumerable<TopMenu> topMenu { get; set; }
        public LHSAllMenu lhsAllMenu { get; set; }
    }

    public class LHSAllMenu
    {
        public IEnumerable<LHSMenu> lhsMenu { get; set; }
        public IEnumerable<SubMenuBrand> lhsSubMenuBrand { get; set; }     
    }
}