using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Entities;
using WebCore.Utils.FilterHelper;

namespace WebCore.Services.Share.Blogs.Dto
{
    //Blog
    public class BlogDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
    }
}
