//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EazyWizy.Domain.Entities
{
    using System;
    
    public partial class Product
    {
        public int product_id { get; set; }
        public string product_type_code { get; set; }
        public string product_name { get; set; }
        public Nullable<decimal> product_price { get; set; }
        public string product_color { get; set; }
        public string product_size { get; set; }
        public string productImageName { get; set; }
        public string product_description { get; set; }
        public string other_product_details { get; set; }
    }
}
