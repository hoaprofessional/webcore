using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.TreeViewHelper
{
    public interface ITreeViewModel
    {
        object Key { get; set; }
        object ParentKey { get; set; }
        List<ITreeViewModel> Childs { get; set; }
        ITreeViewModel Parent { get; set; }
    }
}
