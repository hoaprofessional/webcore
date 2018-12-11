using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MetaDescriptions.Dto
{
    // MetaDescription
    public class MetaDescriptionDto : UpdateTokenModel<int>
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? RecordStatus { get; set; }
    }
}
