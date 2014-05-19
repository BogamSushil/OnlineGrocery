using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataBaseRepository
    {
        #region Category 

        List<Category> GetCategory(long parentCategoryId);
        List<Category> GetAllCategories();
        void AddCategory(Category entity);
        void UpdateCategory(Category entity);
        void RemoveCategory(long id);
        void ActiveDeactiveCategory(long categoryId, bool status);

        #endregion
        #region User

        User LoginUser(string userName, string password);
        string GetForgotPassword(string email);
        void ChangePassword(string userEmail, string newPass, string oldPass);
        User GetUser(string email);
        List<User> GetAllUsers();
        
        #endregion
    }

    public enum DatabaseAction
    {
        None,
        Insert,
        Update,
        Delete,
        Get,
        GetAll,
        DeleteAll,
        ActiveDeactive
    }
}
