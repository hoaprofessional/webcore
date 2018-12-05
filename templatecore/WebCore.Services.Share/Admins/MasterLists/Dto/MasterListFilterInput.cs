using System.ComponentModel.DataAnnotations;
using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterLists.Dto
{
    // MasterList
    public class MasterListFilterInput : IPagingFilterDto
    {
        public MasterListFilterInput()
        {
            RecordStatus = ConstantConfig.RecordStatusConfig.Active;
        }
        [Required(ErrorMessage = "LBL_ADMIN_MENU_GROUP_REQUIRED")]
        public string Group { get; set; }

        [Filter(FilterComparison.Equal)]
        public long? RecordStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
