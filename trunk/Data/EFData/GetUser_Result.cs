//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.EFData
{
    using System;
    
    public partial class GetUser_Result
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string HashId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public byte RoleID { get; set; }
        public string RoleName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string TownCity { get; set; }
        public string Country { get; set; }
        public string OrganisationOrPerson { get; set; }
        public string OrganisationName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
