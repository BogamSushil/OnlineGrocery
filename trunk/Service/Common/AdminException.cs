using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public class AdminException : ApplicationException
    {
        #region "Hard-Coded Error Messages"

        public static readonly string MESSAGE_GENERIC_ERROR =
            "Application has encountered an error, Please contact the Administrator";

        public static readonly string MESSAGE_UNKNOWN =
            "Application has encountered an error (message id: {0})";

        public static readonly string MESSAGE_SECURITY_ERROR =
            "You don't have permissions to view the page or " +
            "to perform the specific action.";
        public static readonly string MESSAGE_AUTHENTICATION = "Invalid login and password or User might be disabled in the system";

        public static readonly string MESSAGE_LOGIN_NEEDED =
            "You must log into the sytem to view the page or " +
            "to perform the specific action.";

        #endregion
        public AdminException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AdminException(string message)
            : base(message, null)
        {
        }

    }

    public class AuthenticationFailedException : AdminException
    {

        public AuthenticationFailedException()
            : base(MESSAGE_AUTHENTICATION, null)
        {
        }

        public AuthenticationFailedException(Exception innerException)
            : base("Faild to authenticate", innerException)
        {
        }
    }
}
