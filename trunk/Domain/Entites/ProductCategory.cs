using Domain.Interfacas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class ProductCategory:IDatabaseObject
    {
        public string product_type_code { get; set; }
        public string parent_product_type_code { get; set; }
        public string product_type_description { get; set; }
        public string productTypeImageName { get; set; }

        public bool Add()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
