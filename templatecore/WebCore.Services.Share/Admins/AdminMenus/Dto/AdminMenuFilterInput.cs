using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.AdminMenus.Dto
{
    // AdminMenu
    public class AdminMenuFilterInput : IPagingFilterDto
    {
        public AdminMenuFilterInput()
        {
            RecordStatus = ConstantConfig.AdminMenuRecordStatus.Active;
        }

        [Filter(FilterComparison.Contains)]
        public string Name { get; set; }

        [Filter(FilterComparison.Contains)]
        public string Link { get; set; }

        [Filter(FilterComparison.Equal)]
        public int? ParentMenuId { get; set; }

        [Filter(FilterComparison.Equal)]
        public long? RecordStatus { get; set; }

        public string[] Permissions { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        [Filter(FilterComparison.Contains, nameof(AdminMenu.Permission))]
        public string PermissionsFilter
        {
            get
            {
                if (Permissions == null)
                {
                    return null;
                }
                else
                {
                    return $",{string.Join(",", Permissions)},";
                }
            }
        }
    }
}
