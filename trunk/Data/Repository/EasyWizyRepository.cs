using Data.Common;
using Domain;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.EFData;
using Domain.Interfaces;

namespace Data.Repository
{
    public class EasyWizyRepository : IDataBaseRepository
    {
        #region Category Master

        public List<Category> GetCategory(long parentCategoryId)
        {
            using(var entities = new EazyWizyEntities())
            {
                var result = entities.GetCategories(parentCategoryId);
                return (from item in result
                    select
                        new Category
                        {
                            CategoryId = item.CategoryId,
                            ImageName = item.ImageName,
                            Description = item.Description,
                            IsActive = item.IsActive.HasValue && item.IsActive.Value,
                            Name = item.Name,
                            ParentCategoryId = item.ParentCategoryId
                        }).ToList();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllCategories();
                return (from item in result
                    select
                        new Category
                        {
                            CategoryId = item.CategoryId,
                            ImageName = item.ImageName,
                            Description = item.Description,
                            IsActive = item.IsActive.HasValue && item.IsActive.Value,
                            Name = item.Name,
                            ParentCategoryId = item.ParentCategoryId
                        }).ToList();
            }
        }

        public void AddCategory(Category entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageCategory(entity.CategoryId, entity.ParentCategoryId,
                    entity.Name, entity.Description, entity.ImageName, entity.IsActive,
                    null, (int) DatabaseAction.Insert);
            }
        }

        public void UpdateCategory(Category entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageCategory(entity.CategoryId, entity.ParentCategoryId,
                    entity.Name, entity.Description, entity.ImageName, entity.IsActive,
                    null, (int)DatabaseAction.Update);
            }
        }

        public void RemoveCategory(long id)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageCategory(id, null, null, null, null, null, null, (int) DatabaseAction.Delete);
            }
        }

        public void ActiveDeactiveCategory(long categoryId, bool status)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageCategory(categoryId, null, null, null,
                    null, status, null, (int)DatabaseAction.ActiveDeactive);
            }
        }

        #endregion

       #region "Product"

        public List<Domain.Product> GetProducts(long categoryId)
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllProducts(categoryId);
                return (from item in result
                    select
                        new Domain.Product
                        {
                            BuyCounter = item.BuyCounter,
                            CategoryId = item.CategoryId,
                            Color = item.Color,
                            Description = item.Description,
                            Image = item.Image,
                            ImageName = item.ImageName,
                            Name = item.Name,
                            OtherProductPetails = item.OtherProductPetails,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Size = item.Size,
                            TypeCode = item.TypeCode,
                        }).ToList();
            }
        }

        public List<Domain.Product> GetAllProdcts()
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllProducts(null);
                return (from item in result
                    select
                        new Domain.Product
                        {
                            BuyCounter = item.BuyCounter,
                            CategoryId = item.CategoryId,
                            Color = item.Color,
                            Description = item.Description,
                            Image = item.Image,
                            ImageName = item.ImageName,
                            Name = item.Name,
                            OtherProductPetails = item.OtherProductPetails,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Size = item.Size,
                            TypeCode = item.TypeCode,
                        }).ToList();
            }
        }

        public void AddProduct(Domain.Product entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageProducts(entity.ProductId, entity.CategoryId, entity.TypeCode, entity.Name, entity.Price,
                    entity.Color, entity.Size, entity.Image, entity.ImageName, entity.Description, entity.BuyCounter,
                    entity.OtherProductPetails, (int) DatabaseAction.Insert);
            }
        }

        public void UpdateProduct(Domain.Product entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageProducts(entity.ProductId, entity.CategoryId, entity.TypeCode, entity.Name, entity.Price,
                    entity.Color, entity.Size, entity.Image, entity.ImageName, entity.Description, entity.BuyCounter,
                    entity.OtherProductPetails, (int)DatabaseAction.Update);
            }
        }

        public void RemoveProduct(long id)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageProducts(id, null, null, null, null, null, null, null, null, null, null, null,
                    (int) DatabaseAction.Delete);
            }
        }

        #endregion

        #region "Brand Master"

        public BrandMaster GetBrand(long brandId)
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllBrandMaster();
                return (from item in result
                    select
                        new BrandMaster
                        {
                            BrandDescription = item.BrandDescription,
                            BrandId = item.BrandId,
                            BrandName = item.BrandName,
                            CreatedBy = item.CreatedBy,
                            IsActive = item.IsActive,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate,
                            CreatedDate = item.CreatedDate,
                            LogoFileName = item.LogoFileName
                        }).SingleOrDefault();
            }
        }

        public List<BrandMaster> GetAllBrands()
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllBrandMaster();
                return (from item in result
                        select
                            new BrandMaster
                            {
                                BrandDescription = item.BrandDescription,
                                BrandId = item.BrandId,
                                BrandName = item.BrandName,
                                CreatedBy = item.CreatedBy,
                                IsActive = item.IsActive,
                                ModifiedBy = item.ModifiedBy,
                                ModifiedDate = item.ModifiedDate,
                                CreatedDate = item.CreatedDate,
                                LogoFileName = item.LogoFileName
                            }).ToList();
            }
        }

        public void AddBrand(BrandMaster entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageBrandMaster(entity.BrandId, entity.BrandName, entity.BrandDescription, entity.IsActive,
                    entity.CreatedBy, entity.CreatedDate, entity.LogoFileName, entity.ModifiedBy,
                    entity.ModifiedDate, (int) DatabaseAction.Insert);
            }
        }

        public void UpdateBrand(BrandMaster entity)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageBrandMaster(entity.BrandId, entity.BrandName, entity.BrandDescription, entity.IsActive,
                    entity.CreatedBy, entity.CreatedDate, entity.LogoFileName, entity.ModifiedBy,
                    entity.ModifiedDate, (int)DatabaseAction.Update);
            }
        }

        public void RemoveBrand(long id)
        {
            using (var entities = new EazyWizyEntities())
            {
                entities.ManageBrandMaster(id, null, null, null, null, null, null, null, null,
                    (int) DatabaseAction.Insert);
            }
        }

        #endregion

        #region "User Services"

        #region User Authentication

        public Domain.User LoginUser(string userName, string password)
        {
            return AuthenticationHelper.Authenticate(userName, password);
        }

        public string GetForgotPassword(string email)
        {
            var pass = string.Empty;
            var user = AuthenticationHelper.Authenticate(email, string.Empty, true);
            if (null != user)
            {
                pass = CryptoServices.EncryptText(user.Password);
            }
            return pass;
        }

        public void ChangePassword(string userEmail, string newPass, string oldPass)
        {
            var user = AuthenticationHelper.Authenticate(userEmail, oldPass);
            if (null != user)
            {
                // DbContext.ManageUserPassword(user.UserID, newPass);
            }
        }
        #endregion
        
        public User GetUser(string email)
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetUser(email);
                return (from item in result
                    select
                        new User()
                        {
                            CreateDate = item.CreateDate,
                            CreatedBy = item.CreatedBy,
                            EmailID = item.EmailID,
                            FirstName = item.FirstName,
                            HashId = item.HashId,
                            IsActive = item.IsActive,
                            LastName = item.LastName,
                            ModifiedBy = item.ModifiedBy,
                            Password = item.Password,
                            ModifiedDate = item.ModifiedDate,
                            RoleID = item.RoleID,
                            RoleName = item.RoleName,
                            UserID = item.UserID
                        }).SingleOrDefault();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var entities = new EazyWizyEntities())
            {
                var result = entities.GetAllUsers();
                return (from item in result
                    select
                        new User()
                        {
                            CreateDate = item.CreateDate,
                            CreatedBy = item.CreatedBy,
                            EmailID = item.EmailID,
                            FirstName = item.FirstName,
                            HashId = item.HashId,
                            IsActive = item.IsActive,
                            LastName = item.LastName,
                            ModifiedBy = item.ModifiedBy,
                            Password = item.Password,
                            ModifiedDate = item.ModifiedDate,
                            RoleID = item.RoleID,
                            RoleName = item.RoleName,
                            UserID = item.UserID
                        }).ToList();
            }
        }
        
        #endregion

        
    }
}
