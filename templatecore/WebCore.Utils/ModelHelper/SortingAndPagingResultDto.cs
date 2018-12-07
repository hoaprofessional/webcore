using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public class SortingAndPagingResultDto<TModel> : PagingResultDto<TModel>, ISortingResultDto
    {
        public string Sorting { get; set; }
    }
}
