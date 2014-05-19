using Data.Common;
using Domain;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        new Category()
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
                        new Category()
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
