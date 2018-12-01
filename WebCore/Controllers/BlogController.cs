using Microsoft.AspNetCore.Mvc;
using WebCore.Models.Blogs;
using WebCore.Services.Share.Blogs;
using WebCore.Services.Share.Blogs.Dto;

namespace WebCore.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult Index(int page = 1)
        {
            BlogViewModel blogViewModel = new BlogViewModel();
            BlogFilterInput blogFilterInput = new BlogFilterInput
            {
                PageSize = 3,
                PageNumber = page
            };
            blogViewModel.Blogs = blogService.GetAllBlogs(blogFilterInput);
            return View(blogViewModel);
        }
    }
}