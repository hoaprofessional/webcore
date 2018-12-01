using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Utils.ModelHelper
{
    public class Auditable : EntityId<int>, IAuditable<int>
    {
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// UserName Created
        /// </summary>
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// UserName Update
        /// </summary>
        [MaxLength(256)]
        public string ModifiedBy { get; set; }
        public long? RecordStatus { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? UpdateToken { get; set; }
    }
}
