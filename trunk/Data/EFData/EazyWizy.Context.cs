﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EazyWizyEntities : DbContext
    {
        public EazyWizyEntities()
            : base("name=EazyWizyEntities")
        {
            var abc = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

       
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<GetCategories_Result> GetCategories(Nullable<long> parentCategory)
        {
            var parentCategoryParameter = parentCategory.HasValue ?
                new ObjectParameter("ParentCategory", parentCategory) :
                new ObjectParameter("ParentCategory", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCategories_Result>("GetCategories", parentCategoryParameter);
        }
    }
}