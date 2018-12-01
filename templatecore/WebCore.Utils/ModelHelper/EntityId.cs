using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public class EntityId<TKey> : IEntityId<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
