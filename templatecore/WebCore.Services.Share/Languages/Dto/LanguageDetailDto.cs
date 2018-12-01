using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Entities;

namespace WebCore.Services.Share.Languages.Dto
{
    public class LanguageDetailDto : IBaseDto<LanguageDetail>
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageKey { get; set; }
        public string LanguageValue { get; set; }
    }
}
