using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{
    public class AuthenticationService : BaseService
    {
        #region "User Authentication"

        public Domain.User LoginUser(string userName, string password)
        {
            return this.Authenticate(userName, password);
        }

        public string GetForgotPassword(string email)
        {
            var pass = string.Empty;
            var user = this.Authenticate(email, string.Empty, true);
            if (null != user)
            {
                pass = CryptoServices.EncryptText(user.Password);
            }
            return pass;
        }

        public void ChangePassword(string userEmail, string newPass, string oldPass)
        {
            var user = Authenticate(userEmail, oldPass);
            if (null != user)
            {
                // DbContext.ManageUserPassword(user.UserID, newPass);
            }
        }

        private Domain.User Authenticate(string login, string password, bool isForgotPasswordRequest = false)
        {

            var user = (new UserService()).GetUser(login);

            if (null == user)
            {
                // Invalid login, authentication failure
                //throw new AuthenticationFailedException();
                throw new System.InvalidOperationException("User Name and/or Password is incorrect");
            }
            else if (!(user.IsActive.HasValue ? user.IsActive.Value : false))
            {
                // Invalid login, authentication failure
                throw new System.InvalidOperationException("User is in Disable mode");
                //throw new AuthenticationFailedException();
            }
            // Match passwords
            else if (!isForgotPasswordRequest && !MatchPassword(user, password))
            {
                // Invalid password, authentication failure
                throw new AuthenticationFailedException();
            }
            else
            {
                return user;
            }
        }

        internal string GeneratePassword(string userName)
        {
            // Create array of 10 letters based on last name, first name
            var builder = new StringBuilder();
            builder.Append(userName);
            if (builder.Length < 10)
            {
                builder.Append("qwertyuiopas");
            }
            var chars = new char[10];
            builder.CopyTo(0, chars, 0, 10);

            builder.Length = 0;

            // Create 6 digit random number to pick letters
            var seed = Utility.GetRandom(999999);
            // Pick letter for each digit
            for (int i = 0; i < 6; i++)
            {
                int digit;
                seed = Math.DivRem(seed, 10, out digit);
                builder.Append(chars[digit]);
            }
            // Append another 5 digit random number
            builder.Append(Utility.GetRandom(99999));
            return builder.ToString();
        }

        private bool ValidatePassword(string password)
        {
            // Enforce password policy
            return !string.IsNullOrEmpty(password);
        }

        private bool MatchPassword(Domain.User user, string password)
        {
            return user.Password == GetPasswordHash(user.HashId, password);
        }

        internal string GetPasswordHash(string hashId, string password)
        {
            // Salt the password with login (login is good choice for salt as 
            // it is immutable field and changes across users)
            var salted = hashId + password + "xyz";
            // Generate the hash
            return CryptoServices.ComputeHash(CryptoServices.HashType.Sha512,
                password);
        }


        #endregion
    }
}
