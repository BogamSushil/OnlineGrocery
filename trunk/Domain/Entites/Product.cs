using Domain.Interfacas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Product : IDatabaseObject
    {
        private IDataBaseRepository _dbRepository;
        public int product_id { get; set; }
        public string product_type_code { get; set; }
        public string product_name { get; set; }
        public Nullable<decimal> product_price { get; set; }
        public string product_color { get; set; }
        public string product_size { get; set; }
        public string productImageName { get; set; }
        public string product_description { get; set; }
        public string other_product_details { get; set; }

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
