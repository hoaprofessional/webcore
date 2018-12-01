using System.Collections.Generic;

namespace WebCore.Utils.ModelHelper
{
    public class PagingResultDto<T> : PagingResultBase
    {
        public PagingResultDto()
        {

        }
        public PagingResultDto(IList<T> items)
        {
            Items = items;
        }
        public IList<T> Items { get; set; }
    }
}
