using System;
using System.Web.Security;

namespace Admin.Portal.Security
{
    public class AdminMembershipUser : MembershipUser
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        #endregion

   
        public AdminMembershipUser(Domain.User user)
            : base("AdminMembershipProvider", user.UserName, user.UserID, user.EmailID, string.Empty, string.Empty, true, false,
            DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserRoleId = user.RoleID.HasValue ? user.RoleID.Value : 0;
            UserRoleName = user.RoleName;
        }
    }
}