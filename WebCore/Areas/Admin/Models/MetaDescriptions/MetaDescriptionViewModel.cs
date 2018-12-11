using WebCore.Services.Share.Admins.MetaDescriptions.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.MetaDescriptions
{
    public class MetaDescriptionViewModel : AdminBaseViewModel
    {
        public MetaDescriptionFilterInput MetaDescriptionFilterInput { get; set; }
        public SortingAndPagingResultDto<MetaDescriptionDto> MainListResult { get; set; }
    }
}
