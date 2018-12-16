using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterLists.Dto
{
    // MasterList
    public class MasterListDto : UpdateTokenModel<int>
    {
        public string Value { get; set; }
        public string Group { get; set; }
        public int? OrderNo { get; set; }
        public string Attribute { get; set; }
        public long? RecordStatus { get; set; }
    }
}
