using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class MasterList : Auditable
    {
        [MaxLength(450)]
        public string Group { get; set; }
        [MaxLength(450)]
        public string Value { get; set; }
        public int? OrderNo { get; set; }
    }
}
