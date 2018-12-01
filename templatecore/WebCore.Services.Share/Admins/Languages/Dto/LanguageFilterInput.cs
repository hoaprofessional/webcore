using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Languages.Dto
{
    public class LanguageFilterInput : IPagingFilterDto
    {
        public string LangCode { get; set; }
        public string LangName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
