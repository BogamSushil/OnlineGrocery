using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class LHSMenu
    {
        public int IdLHSMenu { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public partial class TopMenu
    {
        public int IdMenu { get; set; }
        public string Name { get; set; }
    }

    public partial class SubMenuBrand
    {
        public int IdBrand { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

}
