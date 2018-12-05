using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Services.Share.Blogs.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Blogs
{
    public interface IBlogService
    {
        PagingResultDto<BlogDto> GetAllBlogs(BlogFilterInput blogFilterDto);
        BlogDto GetBlogById(int id);
    }
}
