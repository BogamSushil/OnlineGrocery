using System;

namespace Domain.Entites
{
    public class BrandMaster
    {
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public bool? IsActive { get; set; }
        public string LogoFileName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
