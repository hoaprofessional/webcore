using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.Admins.Users.Dto
{
    public class AssignPermissionInput
    {
        public AssignPermissionInput()
        {
            Permissions = new string[0];
            Roles = new string[0];
        }
        public string UserId { get; set; }
        public string[] Permissions { get; set; }
        public string[] Roles { get; set; }
    }
}
