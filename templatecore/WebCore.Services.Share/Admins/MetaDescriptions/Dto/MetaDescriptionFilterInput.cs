using System.ComponentModel.DataAnnotations;
using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MetaDescriptions.Dto
{
    // MetaDescription
    public class MetaDescriptionFilterInput : IPagingAndSortingFilterDto
    {
        public MetaDescriptionFilterInput()
        {
            RecordStatus = ConstantConfig.RecordStatusConfig.Active;
        }

        [Filter(FilterComparison.Contains)]
        public string Alias { get; set; }
        [Filter(FilterComparison.Equal)]
        public long? RecordStatus { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sorting { get; set; }
    }
}
