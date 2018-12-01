using WebCore.Services.Share.Admins.LanguageDetails.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models
{
    public class LanguageDetailViewModel : AdminBaseViewModel
    {
        public LanguageDetailFilterInput FilterInput { get; set; }
        public PagingResultDto<LanguageDetailDto> PagingResult { get; set; }
        public string LangCode { get; set; }
    }
}
