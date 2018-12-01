using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public interface IEntityId<TKey>
    {
        TKey Id { get; set; }
    }
}
