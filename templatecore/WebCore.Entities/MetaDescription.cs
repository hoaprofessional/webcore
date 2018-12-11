using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    public class MetaDescription : Auditable
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
