using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    // TODO them comment
    public class Blog : Auditable
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string AuthorName { get; set; }

        public ICollection<BlogImage> BlogImages { get; set; }
    }
}
