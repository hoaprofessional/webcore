using Microsoft.AspNetCore.Mvc;
using WebCore.Models.Blogs;
using WebCore.Services.Share.Blogs;
using WebCore.Services.Share.Blogs.Dto;

namespace WebCore.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService blogService;
        public BlogViewModel blogViewModel;
        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
            if(blogViewModel == null)
                blogViewModel = new BlogViewModel();
        }

        public IActionResult Index(int page = 1)
        {
            BlogFilterInput blogFilterInput = new BlogFilterInput
            {
                PageSize = 3,
                PageNumber = page
            };
            blogViewModel.Blogs = blogService.GetAllBlogs(blogFilterInput);
            return View(blogViewModel);
        }
        [ActionName("blogdetail")]
        public IActionResult GetBlogDetail(int id)
        {
            blogViewModel.BlogDto = blogService.GetBlogById(id);
            return View("BlogDetail", blogViewModel);
        }
    }
}