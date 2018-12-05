using WebCore.Services.Share.Admins.MasterLists.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.MasterLists
{
    public class MasterListViewModel : AdminBaseViewModel
    {
        public MasterListFilterInput MasterListFilterInput { get; set; }
        public PagingResultDto<MasterListDto> PagingResult { get; set; }
    }
}
