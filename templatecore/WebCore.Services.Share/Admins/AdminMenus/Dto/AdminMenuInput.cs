using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.AdminMenus.Dto
{
    // AdminMenu
    public class AdminMenuInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_ADMIN_MENU_NAME_REQUIRED")]
        public string Name { get; set; }
        public string Permission { get; set; }
        [Required(ErrorMessage = "LBL_ADMIN_MENU_LINK_REQUIRED")]
        public string Link { get; set; }
        public int? ParentMenuId { get; set; }
        public string Icon { get; set; }
        [Required(ErrorMessage = "LBL_ADMIN_MENU_LINK_REQUIRED")]
        public int? OrderNo { get; set; }
    }
}
