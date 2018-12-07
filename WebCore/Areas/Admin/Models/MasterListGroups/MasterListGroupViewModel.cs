using WebCore.Services.Share.Admins.MasterListGroups.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.MasterListGroups
{
    public class MasterListGroupViewModel : AdminBaseViewModel
    {
        public MasterListGroupFilterInput MasterListGroupFilterInput { get; set; }
        public PagingResultDto<MasterListGroupDto> PagingResult { get; set; }
    }
}
