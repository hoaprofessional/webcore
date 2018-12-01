using System;
using System.ComponentModel.DataAnnotations;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Roles.Dto
{
    /// <summary>
    /// Model WebCoreRole
    /// </summary>
    public class RoleInput : UpdateTokenModel<string>
    {
        [Required(ErrorMessage = "LBL_ROLE_NAME_REQUIRED")]
        public string Name { get; set; }
    }
}
