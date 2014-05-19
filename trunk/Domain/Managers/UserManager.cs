using System.IO;
using Domain.Entites;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public class UserManager
    {
        static readonly IDataBaseRepository repository = IocContainer.Resolve<IDataBaseRepository>();

        public static List<User> GetUsers()
        {
            try
            {
                return repository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static User AuthenticateUser(string emailId, string password)
        {
            try
            {
                return repository.LoginUser(emailId, password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static User GetUser(string emailId)
        {
            try
            {
                return repository.GetUser(emailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
