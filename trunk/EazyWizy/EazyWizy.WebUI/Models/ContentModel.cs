using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EazyWizy.Domain;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.WebUI.Models
{
    public class ContentModel
    {
        public IEnumerable<TopMenu> topMenu { get; set; }
        public LHSAllMenu lhsAllMenu { get; set; }
        public IList<LHSBrandFilter> lhsBrandFilter { get; set; }
    }
    public class LHSAllMenu
    {
        public IEnumerable<LHSMenu> lhsMenu { get; set; }
        public IEnumerable<LHSSubMenuBrand> lhsSubMenuBrand { get; set; }     
    }
}