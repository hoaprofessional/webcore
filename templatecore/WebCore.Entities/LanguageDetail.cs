using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class LanguageDetail : Auditable
    {
        public string LanguageCode { get; set; }
        public string LanguageKey { get; set; }
        public string LanguageValue { get; set; }
    }
}
