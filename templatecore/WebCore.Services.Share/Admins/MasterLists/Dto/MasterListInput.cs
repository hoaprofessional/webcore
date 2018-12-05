using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterLists.Dto
{
    // MasterList
    public class MasterListInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_ADMIN_MENU_GROUP_REQUIRED")]
        public string Group { get; set; }
        [Required(ErrorMessage = "LBL_ADMIN_MENU_VALUE_REQUIRED")]
        public string Value { get; set; }
        public int? OrderNo { get; set; }
    }
}
