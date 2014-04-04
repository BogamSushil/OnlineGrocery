using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Business
{
    public class UserService : BaseService
    {
        #region "Public methods"

       

        public Domain.User GetUser(string emailId)
        {
            return Translate(this.DBContext.GetUser(emailId).FirstOrDefault());
        }

        public void SaveUpdateUser(Domain.User entity)
        {
            DatabaseAction action = entity.UserID == 0 ? DatabaseAction.Insert : DatabaseAction.Update;
            try
            {
                var UserPassword = "Pass@123";
                
                entity.Password = UserPassword;
                this.DBContext.ManageUserOperation(entity.UserID, entity.UserName, entity.FirstName, entity.LastName,
                    entity.EmailID, entity.Password, Guid.NewGuid().ToString(), entity.IsActive, entity.RoleID, entity.CreatedBy, entity.CreateDate, entity.ModifiedBy, entity.ModifiedDate, (int)action);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                this.DBContext.ManageUserOperation(userId, null, null, null,
                    null, null, null, null, null, null, null, null, null, (int)DatabaseAction.Delete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public void ActivateDeactivateUser(int UserId, bool isActive)
        {
            try
            {
                this.DBContext.ManageUserOperation(UserId, null, null, null,
                              null, null, null, isActive, null, null, null, null, null, (int)DatabaseAction.ActiveDeactive);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Private / Helper Methods

        internal static Domain.User Translate(Data.EFData.UserEntity entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.User
            {
                CreateDate = entity.CreateDate,
                CreatedBy = entity.CreatedBy,
                EmailID = entity.EmailID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Password = entity.Password,
                RoleID = entity.RoleID,
                UserID = entity.UserID,
                UserName = entity.UserName,
                HashId = entity.HashId,
                IsActive = entity.IsActive,
                RoleName = entity.RoleName
            };
        }

        #endregion
    }
}
