using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Product
    {
        public long ProductId { get; set; }
        public string TypeCode { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string BuyCounter { get; set; }
        public string OtherProductPetails { get; set; }
        public Int64 CategoryId { get; set; }
    }
}
