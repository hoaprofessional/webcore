using System.ComponentModel.DataAnnotations;
using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterLists.Dto
{
    // MasterList
    public class MasterListFilterInput : IPagingAndSortingFilterDto
    {
        public MasterListFilterInput()
        {
            RecordStatus = ConstantConfig.RecordStatusConfig.Active;
        }
        [Filter(FilterComparison.Equal)]
        public string Group { get; set; }
        [Filter(FilterComparison.Contains)]
        public string Value { get; set; }
        [Filter(FilterComparison.Equal)]
        public long? RecordStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sorting { get; set; }
    }
}
