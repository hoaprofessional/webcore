using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Blogs.Dto
{
    //Blog
    public class BlogFilterInput : IPagingFilterDto
    {
        [Filter(FilterComparison.Contains,"Title")]
        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
