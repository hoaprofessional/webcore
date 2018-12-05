using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.RecordStatuss
{
    public interface IRecordStatusHelper
    {
        SelectList GetRecordStatusCombobox();
    }
}
