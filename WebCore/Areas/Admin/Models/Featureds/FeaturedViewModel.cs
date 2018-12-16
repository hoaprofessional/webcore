using WebCore.Services.Share.Admins.Featureds.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.Featureds
{
    public class FeaturedViewModel : AdminBaseViewModel
    {
        public FeaturedFilterInput FeaturedFilterInput { get; set; }
        public SortingAndPagingResultDto<FeaturedDto> MainListResult { get; set; }
    }
}
