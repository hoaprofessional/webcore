using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.MasterLists
{
    public interface IMasterListProvider
    {
        SelectList SelectItemByGroup(string group);
    }
}
