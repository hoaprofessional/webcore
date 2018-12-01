using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Permissions.Dto;

namespace WebCore.Areas.Admin.Models.Users
{
    public class UserAssignPermissionViewModel
    {
        public PermissionDto TreeViewPermission { get; set; }
        public List<RoleDto> AllRoles { get; set; }
    }
}
