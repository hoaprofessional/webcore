using System.Collections.Generic;
using WebCore.Utils.ModelHelper;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.Services.Share.AdminMenus.Dto
{
    //AdminMenu
    public class AdminMenuTreeViewDto : EntityId<int>, ITreeViewModel
    {
        public string Name { get; set; }
        public string Permission { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public int? ParentMenuId { get; set; }
        public bool IsActive { get; set; }
        public object Key
        {
            get { return Id; }
            set { Id = (int)value; }
        }
        public object ParentKey
        {
            get { return ParentMenuId; }
            set { ParentMenuId = (int)value; }
        }
        public List<ITreeViewModel> Childs { get; set; }
        public ITreeViewModel Parent { get; set; }
    }
}
