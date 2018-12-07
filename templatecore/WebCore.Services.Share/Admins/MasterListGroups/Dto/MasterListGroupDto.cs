using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MasterListGroups.Dto
{
    // MasterList
    public class MasterListGroupDto : UpdateTokenModel<int>
    {
        public string Value { get; set; }
        public int? OrderNo { get; set; }
        public long? RecordStatus { get; set; }
    }
}
