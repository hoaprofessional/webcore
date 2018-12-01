using WebCore.Services.Share.Admins.Languages.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.Languages
{
    public class LanguageViewModel : AdminBaseViewModel
    {
        public LanguageFilterInput LanguageFilterInput { get; set; }
        public PagingResultDto<LanguageDto> PagingResult { get; set; }
    }
}
