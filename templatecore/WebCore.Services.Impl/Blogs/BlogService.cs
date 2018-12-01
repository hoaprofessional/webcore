using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using WebCore.EntityFramework.Repositories;
using WebCore.Entities;
using WebCore.Services.Share.Blogs;
using WebCore.Services.Share.Blogs.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebCore.Utils.FilterHelper;

namespace WebCore.Services.Impl.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog, int> blogRepository;
        private readonly IMapper mapper;

        public BlogService(IRepository<Blog, int> blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        public PagingResultDto<BlogDto> GetAllBlogs(BlogFilterInput blogFilterDto)
        {
            var query = blogRepository.GetAll().ProjectTo<BlogDto>(mapper.ConfigurationProvider);
            query = query.Filter(blogFilterDto);
            return query.PagedQuery(blogFilterDto);
            
        }
    }
}
