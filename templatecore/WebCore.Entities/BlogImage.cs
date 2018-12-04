using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebCore.Entities
{
    public class BlogImage 
    {
        [Key]
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageFile { get; set; }

        [ForeignKey("Blog")]
        public int BlogRefId { get; set; }
        public Blog Blog { get; set; }
    }
}
