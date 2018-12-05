using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.AdminMenus.Dto
{
    // AdminMenu
    public class AdminMenuDto : UpdateTokenModel<int>
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public long? RecordStatus { get; set; }
        public string Icon { get; set; }
        public int? ParentMenuId { get; set; }
        public int? OrderNo { get; set; }
    }
}
