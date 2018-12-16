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
        public string Group { get; set; }
        [Required(ErrorMessage = "LBL_MASTERLIST_VALUE_REQUIRED")]
        public string Value { get; set; }
        public string Attribute { get; set; }
        public int? OrderNo { get; set; }
    }
}
