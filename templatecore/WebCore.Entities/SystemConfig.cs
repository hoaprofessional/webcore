using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class SystemConfig : Auditable
    {
        public string Key { get; set; }
        public string ValueString { get; set; }
        public decimal ValueNumber { get; set; }
    }
}
