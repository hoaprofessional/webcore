using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterListGroups.Dto
{
    // MasterList
    public class MasterListGroupFilterInput : IPagingFilterDto
    {
        public MasterListGroupFilterInput()
        {
            RecordStatus = ConstantConfig.RecordStatusConfig.Active;
        }
        public string Value { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long? RecordStatus { get; set; }
    }
}
