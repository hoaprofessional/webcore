using WebCore.Services.Share.Admins.AdminMenus.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.AdminMenus
{
    public class AdminMenuViewModel : AdminBaseViewModel
    {
        public AdminMenuFilterInput AdminMenuFilterInput { get; set; }
        public PagingResultDto<AdminMenuDto> PagingResult { get; set; }
    }
}
