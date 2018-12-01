using System.Collections.Generic;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.Services.Share.Permissions.Dto
{
    public class PermissionDto : ITreeViewModel
    {
        public object Key { get; set; }
        public object ParentKey { get; set; }
        public List<ITreeViewModel> Childs { get; set; }
        public ITreeViewModel Parent { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public List<string> Roles { get; set; }
        public string RolesDisplay
        {
            get
            {
                return string.Join(",", Roles);
            }
        }
    }
}
