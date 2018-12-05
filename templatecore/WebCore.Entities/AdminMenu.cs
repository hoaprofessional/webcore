using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class AdminMenu : Auditable
    {
        public string Name { get; set; }
        public string Permission { get; set; }
        public string Icon { get; set; }
        public int? ParentMenuId { get; set; }
        public string Link { get; set; }
        public int? OrderNo { get; set; }
    }
}
