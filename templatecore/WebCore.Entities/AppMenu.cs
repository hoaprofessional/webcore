using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class AppMenu : Auditable
    {
        public string MenuName { get; set; }
        public int ParentId { get; set; }
        public string MenuIcon { get; set; }
        public string Permission { get; set; }
    }
}
