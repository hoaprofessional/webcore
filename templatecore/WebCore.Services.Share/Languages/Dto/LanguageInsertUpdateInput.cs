using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Languages.Dto
{
    public class LanguageInsertUpdateInput : EntityId<int>
    {
        public string LanguageCode { get; set; }
        public string LanguageKey { get; set; }
        public string LanguageValue { get; set; }
    }
}
