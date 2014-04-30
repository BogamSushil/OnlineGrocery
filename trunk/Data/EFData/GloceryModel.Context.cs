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
    
    public partial class OnlineGroceryEntities : DbContext
    {
        public OnlineGroceryEntities()
            : base("name=OnlineGroceryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<ProductCategory> GetChieldProductCategorys(string parent_product_type_code)
        {
            var parent_product_type_codeParameter = parent_product_type_code != null ?
                new ObjectParameter("parent_product_type_code", parent_product_type_code) :
                new ObjectParameter("parent_product_type_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductCategory>("GetChieldProductCategorys", parent_product_type_codeParameter);
        }
    
        public virtual ObjectResult<Product> GetProduct(Nullable<int> product_id)
        {
            var product_idParameter = product_id.HasValue ?
                new ObjectParameter("product_id", product_id) :
                new ObjectParameter("product_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product>("GetProduct", product_idParameter);
        }
    
        public virtual ObjectResult<Product> GetProductByCategory(string product_type_code)
        {
            var product_type_codeParameter = product_type_code != null ?
                new ObjectParameter("product_type_code", product_type_code) :
                new ObjectParameter("product_type_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product>("GetProductByCategory", product_type_codeParameter);
        }
    
        public virtual ObjectResult<ProductCategory> GetProductCategory(string product_type_code)
        {
            var product_type_codeParameter = product_type_code != null ?
                new ObjectParameter("product_type_code", product_type_code) :
                new ObjectParameter("product_type_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductCategory>("GetProductCategory", product_type_codeParameter);
        }
    
        public virtual int ManageProduct(Nullable<int> product_id, string product_type_code, string product_name, Nullable<decimal> product_price, string product_color, string product_size, string productImageName, string product_description, string other_product_details, Nullable<int> operation)
        {
            var product_idParameter = product_id.HasValue ?
                new ObjectParameter("product_id", product_id) :
                new ObjectParameter("product_id", typeof(int));
    
            var product_type_codeParameter = product_type_code != null ?
                new ObjectParameter("product_type_code", product_type_code) :
                new ObjectParameter("product_type_code", typeof(string));
    
            var product_nameParameter = product_name != null ?
                new ObjectParameter("product_name", product_name) :
                new ObjectParameter("product_name", typeof(string));
    
            var product_priceParameter = product_price.HasValue ?
                new ObjectParameter("product_price", product_price) :
                new ObjectParameter("product_price", typeof(decimal));
    
            var product_colorParameter = product_color != null ?
                new ObjectParameter("product_color", product_color) :
                new ObjectParameter("product_color", typeof(string));
    
            var product_sizeParameter = product_size != null ?
                new ObjectParameter("product_size", product_size) :
                new ObjectParameter("product_size", typeof(string));
    
            var productImageNameParameter = productImageName != null ?
                new ObjectParameter("productImageName", productImageName) :
                new ObjectParameter("productImageName", typeof(string));
    
            var product_descriptionParameter = product_description != null ?
                new ObjectParameter("product_description", product_description) :
                new ObjectParameter("product_description", typeof(string));
    
            var other_product_detailsParameter = other_product_details != null ?
                new ObjectParameter("other_product_details", other_product_details) :
                new ObjectParameter("other_product_details", typeof(string));
    
            var operationParameter = operation.HasValue ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ManageProduct", product_idParameter, product_type_codeParameter, product_nameParameter, product_priceParameter, product_colorParameter, product_sizeParameter, productImageNameParameter, product_descriptionParameter, other_product_detailsParameter, operationParameter);
        }
    
        public virtual int ManageProductCategory(string product_type_code, string parent_product_type_code, string product_type_description, string productTypeImageName, Nullable<int> operation)
        {
            var product_type_codeParameter = product_type_code != null ?
                new ObjectParameter("product_type_code", product_type_code) :
                new ObjectParameter("product_type_code", typeof(string));
    
            var parent_product_type_codeParameter = parent_product_type_code != null ?
                new ObjectParameter("parent_product_type_code", parent_product_type_code) :
                new ObjectParameter("parent_product_type_code", typeof(string));
    
            var product_type_descriptionParameter = product_type_description != null ?
                new ObjectParameter("product_type_description", product_type_description) :
                new ObjectParameter("product_type_description", typeof(string));
    
            var productTypeImageNameParameter = productTypeImageName != null ?
                new ObjectParameter("productTypeImageName", productTypeImageName) :
                new ObjectParameter("productTypeImageName", typeof(string));
    
            var operationParameter = operation.HasValue ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ManageProductCategory", product_type_codeParameter, parent_product_type_codeParameter, product_type_descriptionParameter, productTypeImageNameParameter, operationParameter);
        }
    
        public virtual int ManageUserOperation(Nullable<int> userID, string userName, string firstName, string lastName, string emailID, string password, string hashId, Nullable<bool> isActive, Nullable<byte> roleID, string createdBy, Nullable<System.DateTime> createDate, string modifiedBy, Nullable<System.DateTime> modifiedDate, Nullable<int> operation)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailIDParameter = emailID != null ?
                new ObjectParameter("EmailID", emailID) :
                new ObjectParameter("EmailID", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var hashIdParameter = hashId != null ?
                new ObjectParameter("HashId", hashId) :
                new ObjectParameter("HashId", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var roleIDParameter = roleID.HasValue ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(byte));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            var createDateParameter = createDate.HasValue ?
                new ObjectParameter("CreateDate", createDate) :
                new ObjectParameter("CreateDate", typeof(System.DateTime));
    
            var modifiedByParameter = modifiedBy != null ?
                new ObjectParameter("ModifiedBy", modifiedBy) :
                new ObjectParameter("ModifiedBy", typeof(string));
    
            var modifiedDateParameter = modifiedDate.HasValue ?
                new ObjectParameter("ModifiedDate", modifiedDate) :
                new ObjectParameter("ModifiedDate", typeof(System.DateTime));
    
            var operationParameter = operation.HasValue ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ManageUserOperation", userIDParameter, userNameParameter, firstNameParameter, lastNameParameter, emailIDParameter, passwordParameter, hashIdParameter, isActiveParameter, roleIDParameter, createdByParameter, createDateParameter, modifiedByParameter, modifiedDateParameter, operationParameter);
        }
    
        public virtual ObjectResult<UserEntity> AuthenticateUser(string emailID, string hashPassword)
        {
            var emailIDParameter = emailID != null ?
                new ObjectParameter("EmailID", emailID) :
                new ObjectParameter("EmailID", typeof(string));
    
            var hashPasswordParameter = hashPassword != null ?
                new ObjectParameter("HashPassword", hashPassword) :
                new ObjectParameter("HashPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserEntity>("AuthenticateUser", emailIDParameter, hashPasswordParameter);
        }
    
        public virtual ObjectResult<UserEntity> GetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserEntity>("GetAllUsers");
        }
    
        public virtual ObjectResult<UserEntity> GetUser(string emailID)
        {
            var emailIDParameter = emailID != null ?
                new ObjectParameter("EmailID", emailID) :
                new ObjectParameter("EmailID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserEntity>("GetUser", emailIDParameter);
        }
    
        public virtual ObjectResult<Product> GetAllProduct(Nullable<int> product_id)
        {
            var product_idParameter = product_id.HasValue ?
                new ObjectParameter("product_id", product_id) :
                new ObjectParameter("product_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product>("GetAllProduct", product_idParameter);
        }
    
        public virtual ObjectResult<ProductCategory> GetAllProductCategory()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductCategory>("GetAllProductCategory");
        }
    
        public virtual ObjectResult<TopMenu> sp_GetTopMenu()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TopMenu>("sp_GetTopMenu");
        }
    
        public virtual ObjectResult<LHSMenu> sp_Select_GetLHSMenu()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LHSMenu>("sp_Select_GetLHSMenu");
        }
    
        public virtual ObjectResult<SubMenuBrand> sp_Select_GetSubMenuBrand()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SubMenuBrand>("sp_Select_GetSubMenuBrand");
        }
    }
}
