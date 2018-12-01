using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public class PagingFilterDto : IPagingFilterDto
    {
        public PagingFilterDto()
        {
            PageSize = -1;
            PageNumber = 1;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
