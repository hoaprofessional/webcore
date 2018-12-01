using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public interface IAuditable<TKey> : IEntityId<TKey>
    {
        DateTime? CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
        long? RecordStatus { get; set; }
        string DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
        Guid? UpdateToken { get; set; }
    }
}
