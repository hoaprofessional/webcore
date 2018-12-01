using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class Language : Auditable
    {
        [MaxLength(100)]
        public string LangCode { get; set; }
        [MaxLength(500)]
        public string LangName { get; set; }
    }
}
