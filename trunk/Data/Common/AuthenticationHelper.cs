using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EFData;
using Data.Repository;
using Domain;

namespace Data.Common
{
    internal class AuthenticationHelper
    {
        internal static User Authenticate(string login, string password, bool isForgotPasswordRequest = false)
        {

            var user = (new EasyWizyRepository()).GetUser(login);

            if (null == user)
            {
                // Invalid login, authentication failure
                //throw new AuthenticationFailedException();
                throw new System.InvalidOperationException("User Name and/or Password is incorrect");
            }
            else if (!(user.IsActive.HasValue && user.IsActive.Value))
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

        internal static string GeneratePassword(string userName)
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

        private static bool ValidatePassword(string password)
        {
            // Enforce password policy
            return !string.IsNullOrEmpty(password);
        }

        private static bool MatchPassword(User user, string password)
        {
            return user.Password == GetPasswordHash(user.HashId, password);
        }

        internal static string GetPasswordHash(string hashId, string password)
        {
            // Salt the password with login (login is good choice for salt as 
            // it is immutable field and changes across users)
            var salted = hashId + password + "xyz";
            // Generate the hash
            return CryptoServices.ComputeHash(CryptoServices.HashType.Sha512,
                password);
        }
    }
}
