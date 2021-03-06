﻿using System.Collections.Generic;
using System.Linq;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Abstract
{
    public interface IMenuRepository
    {
        IEnumerable<TopMenu> TopMenu { get; }

        IList<LHSMenu> LHSMenu { get; }

        IList<LHSSubMenuBrand> LHSSubMenuBrand { get; }

        //This to be changed once the Database Schema is available.
        IList<LHSBrandFilter> LHSBrandFilter { get; }
    }
}
