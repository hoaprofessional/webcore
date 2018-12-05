using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Services.Share.Blogs.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Models.Blogs
{
    public class BlogViewModel
    {
        public PagingResultDto<BlogDto> Blogs { get; set; }
        public BlogFilterInput BlogFilter { get; set; }
        public BlogDto BlogDto { get; set; }
    }
}
