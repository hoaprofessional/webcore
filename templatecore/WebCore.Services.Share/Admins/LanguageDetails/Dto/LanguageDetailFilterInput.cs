using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.LanguageDetails.Dto
{
    public class LanguageDetailFilterInput : IPagingFilterDto
    {
        public string LangCode { get; set; }
        [Filter(FilterComparison.Contains)]
        public string LanguageKey { get; set; }
        [Filter(FilterComparison.Contains)]
        public string LanguageValue { get; set; }
        public int PageNumber { get ; set ; }
        public int PageSize { get ; set ; }
    }
}
