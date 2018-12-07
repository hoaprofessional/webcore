using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterListGroups.Dto
{
    // MasterList
    public class MasterListGroupInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_MASTER_LIST_GROUP_VALUE_REQUIRED")]
        public string Value { get; set; }
        [Required(ErrorMessage = "LBL_MASTER_LIST_GROUP_ORDER_NO_REQUIRED")]
        public int? OrderNo { get; set; }
    }
}
