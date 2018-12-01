using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public class UpdateTokenModel<TKey> : EntityId<TKey>
    {
        public Guid UpdateToken { get; set; }
    }
}
